using NUnit.Framework;
using System;
using System.IO;
using AdminUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminUI.Data;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoFixture;

namespace AdminUI.Tests
{

    [TestFixture]
    public class HomeControllerTests
    {

        private readonly TimesheetContext _dbcontext;
        private readonly Controller _controller;
        private readonly ILogger<HomeController> _logger;

        public HomeControllerTests()
        {
            _dbcontext = new InMemoryDbContextFactory().GetTimesheetContext();
            _logger = new NullLogger<HomeController>();
            _controller = new HomeController(_logger, _dbcontext);
        }

       [Test]
        public void IndexTest_IsNotNull()
        {
            var hc = new HomeController(_logger, _dbcontext);
            var result = (ViewResult)hc.Index("sortOrder", "pName", "cName", "01/01/2020", "01/14/2020", "123456", "1", "P123456");
            Assert.IsNotNull(result.ViewData);
        }
    }
}