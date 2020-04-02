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

        public IActionResult Index(string sortOrder, string pName, string cName, string dateFrom, string dateTo, string prime, string id)
        {
            var sheets = GetSheets();

            var model = new HomeModel();
            
            //filter the timesheets 
            if (!string.IsNullOrEmpty(pName))
            {
                model.PName = pName;
                sheets = sheets.Where(t => t.ProviderName.ToLower().Contains(pName.ToLower()));
            }

            if (!string.IsNullOrEmpty(cName))
            {
                model.CName = cName;
                sheets = sheets.Where(t => t.ClientName.ToLower().Contains(cName.ToLower()));
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

            if (!string.IsNullOrEmpty(prime))
            {
                model.Prime = int.Parse(prime);
                sheets = sheets.Where(t => t.ClientPrime == prime);
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
    }
}
