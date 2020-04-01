using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminUI.Models;
using Amazon.DeviceFarm.Model;
using Microsoft.EntityFrameworkCore.Internal;

namespace AdminUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string sortOrder, string pName, string cName, string dateFrom, string dateTo, string prime, string id)
        {
            var sheets = GetTestSheets();
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
                sheets = sheets.Where(t => t.Prime == int.Parse(prime));
            }

            if (!string.IsNullOrEmpty(id))
            {
                model.Id = int.Parse(id);
                sheets = sheets.Where(t => t.ID == int.Parse(id));
            }

            //big ol' switch statement determines how to sort the data in the table
            switch (sortOrder)
            { 
                case "id":
                    sheets = sheets.OrderBy(t => t.ID);
                    break;
                case "id_desc":
                    sheets = sheets.OrderByDescending(t => t.ID);
                    break;
                case "pname":
                    sheets = sheets.OrderBy(t => t.ProviderName);
                    break;
                case "pname_desc":
                    sheets = sheets.OrderByDescending(t => t.ProviderName);
                    break;
                case "prime":
                    sheets = sheets.OrderBy(t => t.Prime);
                    break;
                case "prime_desc":
                    sheets = sheets.OrderByDescending(t => t.Prime);
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
                    sheets = sheets.OrderBy(t => t.ID);
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

        //generate some fake time sheets
        private static IEnumerable<Timesheet> GetTestSheets()
        {
            var t = new List<Timesheet>
            {
                new Timesheet { ID = 1, ClientName = "Mickey Mouse", ProviderName = "Minnie Mouse", Prime = 12345, Submitted = DateTime.Parse("2020-03-23") , Hours = 30.0},
                new Timesheet { ID = 2, ClientName = "Huey Duck", ProviderName = "Donald Duck", Prime = 4444, Submitted = DateTime.Parse("2020-03-21"), Hours = 35.3 },
                new Timesheet { ID = 3, ClientName = "Dewey Duck", ProviderName = "Donald Duck", Prime = 4444, Submitted = DateTime.Parse("2020-03-21"), Hours = 62.0  },
                new Timesheet { ID = 4, ClientName = "Louie Duck", ProviderName = "Donald Duck", Prime = 4444, Submitted = DateTime.Parse("2020-03-22") , Hours = 87.5 },
                new Timesheet { ID = 5, ClientName = "Anna", ProviderName = "Elsa", Prime = 654654, Submitted = DateTime.Parse("2020-03-23") , Hours = 10.3 },
                new Timesheet { ID = 6, ClientName = "Beast", ProviderName = "Belle", Prime = 899877, Submitted = DateTime.Parse("2020-03-23") , Hours = 45.6 },
                new Timesheet { ID = 7, ClientName = "Ariel", ProviderName = "King Triton", Prime = 987987, Submitted = DateTime.Parse("2020-03-20") , Hours = 35.4 },
                new Timesheet { ID = 8, ClientName = "Snow White", ProviderName = "Prince Florian", Prime = 1111, Submitted = DateTime.Parse("2020-03-21") , Hours = 48.2 },
                new Timesheet { ID = 9, ClientName = "Cinderella", ProviderName = "Prince Charming", Prime = 465444, Submitted = DateTime.Parse("2020-03-21") , Hours = 49.2 },
                new Timesheet { ID = 10, ClientName = "Shrek", ProviderName = "Fiona", Prime = 432134, Submitted = DateTime.Parse("2020-03-23") , Hours = 5.2 },
                new Timesheet { ID = 11, ClientName = "Gingerbread Man", ProviderName = "Lord Farquaad", Prime = 60054, Submitted = DateTime.Parse("2020-03-23"), Hours = 3.3  },
                new Timesheet { ID = 12, ClientName = "Aladdin", ProviderName = "Jasmine", Prime = 8977, Submitted = DateTime.Parse("2020-03-23") , Hours = 35.4 },
                new Timesheet { ID = 13, ClientName = "Abu", ProviderName = "Genie", Prime = 4000, Submitted = DateTime.Parse("2020-03-19") , Hours = 5.3 },
                new Timesheet { ID = 14, ClientName = "Flynn Rider", ProviderName = "Rapunzel", Prime = 441234, Submitted = DateTime.Parse("2020-03-21") , Hours = 25.3 },
                new Timesheet { ID = 15, ClientName = "Pascal", ProviderName = "Rapunzel", Prime = 441234, Submitted = DateTime.Parse("2020-03-21") , Hours = 55.3 },
                new Timesheet { ID = 16, ClientName = "Maximus", ProviderName = "Rapunzel", Prime = 441234, Submitted = DateTime.Parse("2020-03-22") , Hours = 45.3 },
                new Timesheet { ID = 17, ClientName = "Max", ProviderName = "Goofy", Prime = 6654, Submitted = DateTime.Parse("2020-03-23") , Hours = 66.3 },
                new Timesheet { ID = 18, ClientName = "Nala", ProviderName = "Simba", Prime = 330077, Submitted = DateTime.Parse("2020-03-23") , Hours = 35.3 },
                new Timesheet { ID = 19, ClientName = "Angel", ProviderName = "Stitch", Prime = 98890, Submitted = DateTime.Parse("2020-03-20") , Hours = 77.3 },
                new Timesheet { ID = 20, ClientName = "Dalmation 1", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20") , Hours = 35.5 },
                new Timesheet { ID = 21, ClientName = "Dalmation 2", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20"), Hours = 35.4  },
                new Timesheet { ID = 22, ClientName = "Dalmation 3", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20") , Hours = 32.3 },
                new Timesheet { ID = 23, ClientName = "Dalmation 4", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20") , Hours = 31.3 },
                new Timesheet { ID = 24, ClientName = "Dalmation 5", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20") , Hours = 30.3 },
                new Timesheet { ID = 25, ClientName = "Dalmation 6", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20") , Hours = 39.3 },
                new Timesheet { ID = 26, ClientName = "Dalmation 7", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20") , Hours = 37.3 },
                new Timesheet { ID = 27, ClientName = "Dalmation 8", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20") , Hours = 38.3 },
                new Timesheet { ID = 28, ClientName = "Dalmation 9", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20") , Hours = 34.3 },
                new Timesheet { ID = 29, ClientName = "Dalmation 10", ProviderName = "Dalmation Dad", Prime = 101, Submitted = DateTime.Parse("2020-03-20"), Hours = 25.3  }
            };
            for (int i = 30; i < 1000; i++)
            {
                t.Add(new Timesheet
                {
                    ID = i, ClientName = "Dalmation " + i.ToString(), ProviderName = "Dalmation Dad", Prime = 101,
                    Submitted = DateTime.Parse("2020-03-20"), Hours = 25.3
                });
            }

            return t;

        }

        //should return a Timesheet View
        public IActionResult Timesheet(int ID)
        {
            ViewData["Sheet"] = GetTestSheets().Where(t => t.ID == ID).ElementAt(0);
            return View();
        }
    }
}