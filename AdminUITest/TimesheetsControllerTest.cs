using NUnit.Framework;
using AdminUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using AdminUI.Data;

namespace AdminUI.Tests
{

    [TestFixture]
    public class TimesheetsControllerTests
    {

        private readonly TimesheetContext _dbcontext;
        private readonly Controller _controller;

        public TimesheetsControllerTests()
        {
            _dbcontext = new InMemoryDbContextFactory().GetTimesheetContext();
            _controller = new TimesheetsController(_dbcontext);
        }

       [Test]
        public async System.Threading.Tasks.Task IndexTest_IsNotNullAsync()
        {
            var tsc = new TimesheetsController(_dbcontext);
            var result = (ViewResult)await tsc.Index();
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateTest_IsNotNull()
        {
            var tsc = new TimesheetsController(_dbcontext);
            var result = (ViewResult)tsc.Create();
            Assert.IsNotNull(result);
        }
    }
}