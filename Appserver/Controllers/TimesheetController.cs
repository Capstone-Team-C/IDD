using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Appserver.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace Appserver.Controllers
{
    public class TimesheetController : Controller
    {
        [Produces("application/json")]
        public IActionResult Ready()
        {
            TimesheetForm model = new TimesheetForm();
            
            return Json(model);
        }

        private class JsonResponse
        {
            public string response = "ok";
        }
        [Produces("application/json")]
        public IActionResult Validate()
        {
            JsonResponse model = new JsonResponse();
            return Json(model);
        }

        [Produces("application/json")]
        public IActionResult Submit()
        {
            JsonResponse model = new JsonResponse();
            return Json(model);
        }

        [Produces("application/json")]
        public IActionResult Received()
        {
            JsonResponse model = new JsonResponse();
            return Json(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
