using NUnit.Framework;
using AdminUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminUI.Data;
using Microsoft.Extensions.Logging.Abstractions;

namespace AdminUITest
{

    [TestFixture]
    public class HomeControllerTests
    {

        private readonly ILogger<HomeController> _logger;
        private readonly SubmissionContext _scontext;
        private readonly UserContext _ucontext;

        public HomeControllerTests()
        {
            _logger = new NullLogger<HomeController>();
            _scontext = new InMemoryDbContextFactory().GetSubmissionContext();
            _ucontext = new InMemoryDbContextFactory().GetUserContext();
        }

       [Test]
        public void IndexTest_IsNotNull()
        {
            var hc = new HomeController(_logger, _scontext, _ucontext);
            var result = (ViewResult)hc.Index("sortOrder", "pName", "cName", "01/01/2020", "01/14/2020", "123456", "1", "P1234");
            Assert.IsNotNull(result.ViewData);
        }
    }
}