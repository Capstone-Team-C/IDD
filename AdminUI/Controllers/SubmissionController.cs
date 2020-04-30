﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdminUI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Common.Data;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System.IO;

namespace AdminUI.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly ILogger<SubmissionController> _logger;
        private readonly SubmissionContext _scontext;


        public SubmissionController(ILogger<SubmissionController> logger, SubmissionContext scontext)
        {
            _logger = logger;
            _scontext = scontext;
        }

        public async Task<IActionResult> GenPDF(int id)
        {
            //find the timesheet code
            var ts = await _scontext.Submissions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ts == null)
                return NotFound();
            //http://www.pdfsharp.net/wiki/Unicode-sample.ashx
            string pdfString = "eXPRS Plan of Care - Services Delivered Report Form\n\n" +
                                "Timesheet ID: " + ts.Id + "\n" +
                                "Status of Timesheet: " + ts.Status + "\n" +
                                "Customer Name: " + ts.ClientName + "\n" + 
                                "Prime: " + ts.ClientPrime + "\n" +
                                "Provider Name: " + ts.ProviderName + "\n" +
                                "Provider Num: " + ts.ProviderId + "\n" +
                                "CM Organization: Multnomah Case Management\n" + 
                                "Service Goal: " + ts.ServiceGoal + "\n" + 
                                "Submitted on: " + ts.Submitted + "\n" +
                                "Form Type: " + ts.FormType + "\n";
            // Create new document
            PdfDocument document = new PdfDocument();

            // Set font encoding to unicode
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);

            XFont font = new XFont("Times New Roman", 12, XFontStyle.Regular, options);

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;

            tf.DrawString(pdfString, font, XBrushes.Black,
              new XRect(100, 100, page.Width - 200, 600), XStringFormats.TopLeft);

            //This code section will add new page with image.
            PdfPage page2 = document.AddPage();
            //TODO: This will eventually get the image from the database, whether that's S3 or something else.
            XImage front = XImage.FromFile("wwwroot/images/timesheet-front.PNG");
            XGraphics gfx2 = XGraphics.FromPdfPage(page2);
            gfx2.DrawImage(front, 10, page2.Height/8, front.Width * .5, front.Height * .5);

            PdfPage page3 = document.AddPage();
            XImage back = XImage.FromFile(@"C:\Users\larry\source\repos\IDD\AdminUI\wwwroot\images\timesheet-back.PNG");
            XGraphics gfx3 = XGraphics.FromPdfPage(page3);
            gfx3.DrawImage(back, 10, page3.Height/8, back.Width * .5, back.Height * .5);

            MemoryStream stream = new MemoryStream();
            // Save the document
            document.Save(stream, true);
            string fileDownloadName = ts.ClientName + "_" + ts.ClientPrime + "_" + ts.ProviderId + "_" + ts.ProviderName + "_" + ts.Submitted + "_" + ts.FormType + ".pdf";
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, fileDownloadName);
        }

        [HttpPost]
        public async Task<IActionResult> Process(int id, string Status, string RejectionReason)
        {
            var submission = _scontext.Submissions.Find(id);

            if (submission == null)
                return NotFound();
            
            _scontext.Entry(submission).Reference(t => t.LockInfo).Load();
            
            if (submission.LockInfo == null || !submission.LockInfo.User.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase))
                return View("NoPermission");
            
            submission.Status = Status;
            submission.RejectionReason = RejectionReason;
            submission.UserActivity = Status + " by " + submission.LockInfo.User + " on " + DateTime.Now;
            submission.LockInfo = null;
            
            if (ModelState.IsValid)
            {
                try
                {
                    _scontext.Update(submission);
                    await _scontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!_scontext.Submissions.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index","Home");
        }
    }
}
