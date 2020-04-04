using NUnit.Framework;
using System;
using System.IO;
using mvc_trial.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace mvc_trialTest
{
    public class AdminControllereTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AdminDashboardTest()
        {
            AdminController adc = new AdminController();
            var result = (ViewResult)adc.Dashboard();
            Assert.IsNotNull(result.ViewData);
        }

        [Test]
        public void AdminFilterDateTest()
        {
            AdminController adc = new AdminController();
            var result = (JsonResult)adc.FilterDate("today", "yesterday");
            Assert.IsNotEmpty(result.ToString());
        }

        [Test]
        public void AdminFilterNameTest()
        {
            AdminController adc = new AdminController();
            var result = (JsonResult)adc.FilterName("Oranssi", "Pazuzu");
            Assert.IsNotEmpty(result.ToString());
        }

    }
}