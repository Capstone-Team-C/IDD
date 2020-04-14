using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdminUI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminUI.Models;
using Amazon.DeviceFarm.Model;
using Amazon.DirectConnect.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SQLitePCL;

namespace AdminUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TimesheetContext _context;

        public HomeController(ILogger<HomeController> logger, TimesheetContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(string sortOrder, string pName, string cName, string dateFrom, string dateTo, string prime, string id, string providerId)
        {
            var sheets = GetSheets();

            var model = new HomeModel();
            
            //filter the timesheets 
            if (!string.IsNullOrEmpty(pName))
            {
                model.PName = pName;
                sheets = sheets.Where(t => t.ProviderName.ToLower().Contains(pName.ToLower()));
            }
            if (!string.IsNullOrEmpty(providerId))
            {
                model.ProviderId = providerId;
                sheets = sheets.Where(t => t.ProviderID.ToLower().Contains(providerId.ToLower()));
            }

            if (!string.IsNullOrEmpty(cName))
            {
                model.CName = cName;
                sheets = sheets.Where(t => t.ClientName.ToLower().Contains(cName.ToLower()));
            }
            if (!string.IsNullOrEmpty(prime))
            {
                model.Prime = prime;
                sheets = sheets.Where(t => t.ClientPrime.ToLower().Contains(prime.ToLower()));
            }
            if (!string.IsNullOrEmpty(dateFrom))
            {
                model.DateFrom = dateFrom;
                sheets = sheets.Where(t => t.Submitted >= DateTime.Parse(dateFrom));
            }
            if (!string.IsNullOrEmpty(dateTo))
            {
                model.DateTo = dateTo;
                sheets = sheets.Where(t => t.Submitted <= DateTime.Parse(dateTo));
            }
            if (!string.IsNullOrEmpty(id))
            {
                model.Id = int.Parse(id);
                sheets = sheets.Where(t => t.TimesheetID == int.Parse(id));
            }

            //big ol' switch statement determines how to sort the data in the table
            switch (sortOrder)
            { 
                case "id":
                    sheets = sheets.OrderBy(t => t.TimesheetID);
                    break;
                case "id_desc":
                    sheets = sheets.OrderByDescending(t => t.TimesheetID);
                    break;
                case "pname":
                    sheets = sheets.OrderBy(t => t.ProviderName);
                    break;
                case "pname_desc":
                    sheets = sheets.OrderByDescending(t => t.ProviderName);
                    break;
                case "prime":
                    sheets = sheets.OrderBy(t => t.ClientPrime);
                    break;
                case "prime_desc":
                    sheets = sheets.OrderByDescending(t => t.ClientPrime);
                    break;
                case "cname":
                    sheets = sheets.OrderBy(t => t.ClientName);
                    break;
                case "cname_desc":
                    sheets = sheets.OrderByDescending(t => t.ClientName);
                    break;
                case "date":
                    sheets = sheets.OrderBy(t => t.Submitted);
                    break;
                case "date_desc":
                    sheets = sheets.OrderByDescending(t => t.Submitted);
                    break;
                case "hours":
                    sheets = sheets.OrderBy(t => t.Hours);
                    break;
                case "hours_desc":
                    sheets = sheets.OrderByDescending(t => t.Hours);
                    break;
                case "providerid":
                    sheets = sheets.OrderBy(t => t.ProviderID);
                    break;
                case "providerid_desc":
                    sheets = sheets.OrderByDescending(t => t.ProviderID);
                    break;
                default:
                    sheets = sheets.OrderBy(t => t.TimesheetID);
                    break;
            }

            model.SortOrder = sortOrder;
            model.Sheets = new List<Timesheet>(sheets);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<Timesheet> GetSheets()
        {
            return  _context.Timesheet.AsEnumerable();
        }

        //should return a Timesheet View
        public IActionResult Timesheet(int ID)
        {
            Timesheet timesheet =  _context.Timesheet.Find(ID);
            timesheet.Shifts = new List<Shift>
            {
                new Shift
                {
                    Date = DateTime.Parse("3/11/2020"),
                    In = DateTime.Parse("11:30 AM"),
                    Out = DateTime.Parse("7:30 PM"),
                    Hours = 8.00,
                    Group = false
                },
                new Shift
                {
                    Date = DateTime.Parse("3/12/2020"),
                    In = DateTime.Parse("11:30 AM"),
                    Out = DateTime.Parse("7:30 PM"),
                    Hours = 8.00,
                    Group = false
                }
            };

            ViewData["sheet"] = timesheet;
            return View();
        }

        public FileContentResult DownloadCSV(string pName, string cName, string dateFrom, string dateTo, string prime, string id)
        {
            var sheets = GetSheets();

            
            //filter the timesheets 
            if (!string.IsNullOrEmpty(pName))
                sheets = sheets.Where(t => t.ProviderName.ToLower().Contains(pName.ToLower()));

            if (!string.IsNullOrEmpty(cName))
                sheets = sheets.Where(t => t.ClientName.ToLower().Contains(cName.ToLower()));

            if (!string.IsNullOrEmpty(dateFrom))
                sheets = sheets.Where(t => t.Submitted >= DateTime.Parse(dateFrom));

            if (!string.IsNullOrEmpty(dateTo))
                sheets = sheets.Where(t => t.Submitted <= DateTime.Parse(dateTo));

            if (!string.IsNullOrEmpty(prime))
                sheets = sheets.Where(t => t.ClientPrime == prime);

            if (!string.IsNullOrEmpty(id))
                sheets = sheets.Where(t => t.TimesheetID == int.Parse(id));

            //the following loops through every property in a timesheet, first saving the names of the properties to 
            //act as a header. Then, it loops through every timesheet, adding every individual property of the timesheet
            //to the csv, then returning it for download.
            var properties = typeof(Timesheet).GetProperties();
            var csv = properties.Aggregate("", (current, f) => current + (f.Name + ','));
            foreach (var s in sheets)
            {
                csv += '\n';
                foreach (var p in properties)
                {
                    
                    if (p.GetValue(s) != null)
                        csv += "\"" + p.GetValue(s).ToString().Replace("\"","\"\"") + "\"";
                    csv += ',';
                }
            }
            var name = "Submissions_summary_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss") + ".csv";
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", name);
        }

        [HttpPost]
        public async Task<IActionResult> Process(int id, string Status, string RejectionReason)
        {
            var timesheet = await _context.Timesheet
                .FirstOrDefaultAsync(m => m.TimesheetID == id);
            if (timesheet == null)
                return NotFound();
            timesheet.Status = Status;
            timesheet.RejectionReason = RejectionReason;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timesheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!_context.Timesheet.Any(e => e.TimesheetID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
