﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminUI.Models;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Common.Data;
using Microsoft.AspNetCore.Authorization;
using SQLitePCL;

namespace AdminUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SubmissionContext _context;

        public HomeController(ILogger<HomeController> logger, SubmissionContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string sortOrder = "id", string pName="", string cName="", string dateFrom="", string dateTo="", string prime="", string providerId="", string status="pending", int page = 1, int perPage = 20, string formType="timesheet")
        {
            var submissions = GetSubmissions(formType);

            var model = new HomeModel{FormType = formType};
            
            //filter the timesheets 
            if (!string.IsNullOrEmpty(pName))
            {
                model.PName = pName;
                submissions = submissions.Where(t => t.ProviderName.ToLower().Contains(pName.ToLower()));
            }
            if (!string.IsNullOrEmpty(providerId))
            {
                model.ProviderId = providerId;
                submissions = submissions.Where(t => t.ProviderId.ToLower().Contains(providerId.ToLower()));
            }

            if (!string.IsNullOrEmpty(cName))
            {
                model.CName = cName;
                submissions = submissions.Where(t => t.ClientName.ToLower().Contains(cName.ToLower()));
            }
            if (!string.IsNullOrEmpty(prime))
            {
                model.Prime = prime;
                submissions = submissions.Where(t => t.ClientPrime.Contains(prime, StringComparison.CurrentCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(dateFrom))
            {
                model.DateFrom = dateFrom;
                submissions = submissions.Where(t => t.Submitted >= DateTime.Parse(dateFrom));
            }
            if (!string.IsNullOrEmpty(dateTo))
            {
                model.DateTo = dateTo;
                submissions = submissions.Where(t => t.Submitted <= DateTime.Parse(dateTo));
            }

            if(!string.Equals(status,"all",StringComparison.CurrentCultureIgnoreCase))
                submissions = submissions.Where(t => t.Status.Equals(status,StringComparison.CurrentCultureIgnoreCase));

            
            model.Status = status;

            //big ol' switch statement determines how to sort the data in the table
            submissions = sortOrder switch
            {
                "id" => submissions.OrderBy(t => t.Id),
                "id_desc" => submissions.OrderByDescending(t => t.Id),
                "pname" => submissions.OrderBy(t => t.ProviderName),
                "pname_desc" => submissions.OrderByDescending(t => t.ProviderName),
                "prime" => submissions.OrderBy(t => t.ClientPrime),
                "prime_desc" => submissions.OrderByDescending(t => t.ClientPrime),
                "cname" => submissions.OrderBy(t => t.ClientName),
                "cname_desc" => submissions.OrderByDescending(t => t.ClientName),
                "date" => submissions.OrderBy(t => t.Submitted),
                "date_desc" => submissions.OrderByDescending(t => t.Submitted),
                "ProviderId" => submissions.OrderBy(t => t.ProviderId),
                "ProviderId_desc" => submissions.OrderByDescending(t => t.ProviderId),
                _ => submissions.OrderBy(t => t.Id),
            };

            model.SortOrder = sortOrder;
            model.TotalSubmissions = submissions.Count();
            model.TotalPages = model.TotalSubmissions / perPage + (model.TotalSubmissions % perPage == 0 ? 0 : 1);
            submissions = submissions.Skip((page - 1) * perPage).Take(perPage);
            model.PerPage = perPage;
            model.Page = page;

            foreach (var sub in submissions)
            {
                sub.LoadEntries(_context);
                _context.Entry(sub).Reference(s => s.LockInfo).Load();
            }

            model.Submissions = new List<Submission>(submissions);
            return View(formType + "Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<Submission> GetSubmissions(string formType)
        {
            if (formType.Equals("timesheet"))
                return _context.Timesheets.AsEnumerable();
            return _context.MileageForms.AsEnumerable();
        }

        public bool GetLockInfo(int id)
        {
            var submission = _context.Submissions.Find(id);
            _context.Entry(submission).Reference(t => t.LockInfo).Load();

            //if lock exists, disable processing and indicate sheet is locked
            //else no lock, create lock.
            if (submission.LockInfo == null)
            {

                submission.LockInfo = new Lock
                {
                    LastActivity = DateTime.Now,
                    User = User.Identity.Name
                };

                _context.Update(submission);
                _context.SaveChanges();
            }

            return submission.LockInfo.User.Equals(User.Identity.Name);
        }

        //Releases the Lock if the current User is holding the lock
        public void ReleaseLock(int id)
        {
            var submission = _context.Submissions.Find(id);
            _context.Entry(submission).Reference(t => t.LockInfo).Load();

            if (submission.LockInfo.User.Equals(User.Identity.Name))
            {
                submission.LockInfo = null;
                _context.Update(submission);
                _context.SaveChanges();
            }
        }

        public FileContentResult DownloadCSV(string pName, string cName, string dateFrom, string dateTo, string prime, string id, string status, string formType)
        {
            var submissions = GetSubmissions(formType);

            //filter the timesubmissions 
            if (!string.IsNullOrEmpty(pName))
                submissions = submissions.Where(t => t.ProviderName.ToLower().Contains(pName.ToLower()));

            if (!string.IsNullOrEmpty(cName))
                submissions = submissions.Where(t => t.ClientName.ToLower().Contains(cName.ToLower()));

            if (!string.IsNullOrEmpty(dateFrom))
                submissions = submissions.Where(t => t.Submitted >= DateTime.Parse(dateFrom));

            if (!string.IsNullOrEmpty(dateTo))
                submissions = submissions.Where(t => t.Submitted <= DateTime.Parse(dateTo));

            if (!string.IsNullOrEmpty(prime))
                submissions = submissions.Where(t => t.ClientPrime == prime);

            if (!string.IsNullOrEmpty(id))
                submissions = submissions.Where(t => t.Id == int.Parse(id));

            if (string.IsNullOrEmpty(status))
                status = "pending";

            if(!string.Equals(status,"all",StringComparison.CurrentCultureIgnoreCase))
                submissions = submissions.Where(t => t.Status.Equals(status,StringComparison.CurrentCultureIgnoreCase));

            //the following loops through every property in a submission, first saving the names of the properties to 
            //act as a header. Then, it loops through every submission, adding every individual property of the submission
            //to the csv, then returning it for download.
            
            //var properties = typeof(Timesheet).GetProperties();
            var properties = submissions.GetType().GetGenericArguments()[0].GetProperties();
            var csv = properties.Aggregate("", (current, f) => current + (f.Name + ','));
            foreach (var s in submissions)
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

    }
}
