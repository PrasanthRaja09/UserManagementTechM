using Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserManagement;
using UserManagement.Controllers;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private IRepositoryWrapper _repoWrapper;
        private IConfiguration _configuration;
        private ILoggerManager _logger;

        [TestInitialize]
        public void Initalize(IRepositoryWrapper repoWrapper, IConfiguration iConfig, ILoggerManager logger)
        {
            _repoWrapper = repoWrapper;
            _configuration = iConfig;
            _logger = logger;
        }

       

        [TestMethod]
        public void TestMethod1()
        {
            int x = 200;
            AuthController _controller = new AuthController(_repoWrapper, _configuration, _logger);
            var ontroller = _controller.Version();
            x.Equals(_controller);
        }
    }
}
