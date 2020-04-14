using NUnit.Framework;
using System;
using System.IO;
using Appserver.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Appserver.Tests
{
    [TestFixture]
    public class AdminControllereTests
    {
        

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
            Assert.IsTrue(result.ToString() != "");
        }

        [Test]
        public void AdminFilterNameTest()
        {
            AdminController adc = new AdminController();
            var result = (JsonResult)adc.FilterName("Oranssi", "Pazuzu");
            Assert.IsTrue(result.ToString() != "");
        }

    }
}