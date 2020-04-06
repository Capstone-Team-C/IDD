using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using mvc_trial.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace mvc_trialTest
{
    [TestClass]
    public class AdminControllereTests
    {
        

        [TestMethod]
        public void AdminDashboardTest()
        {
            AdminController adc = new AdminController();
            var result = (ViewResult)adc.Dashboard();
            Assert.IsNotNull(result.ViewData);
        }

        [TestMethod]
        public void AdminFilterDateTest()
        {
            AdminController adc = new AdminController();
            var result = (JsonResult)adc.FilterDate("today", "yesterday");
            Assert.IsTrue(result.ToString() != "");
        }

        [TestMethod]
        public void AdminFilterNameTest()
        {
            AdminController adc = new AdminController();
            var result = (JsonResult)adc.FilterName("Oranssi", "Pazuzu");
            Assert.IsTrue(result.ToString() != "");
        }

    }
}