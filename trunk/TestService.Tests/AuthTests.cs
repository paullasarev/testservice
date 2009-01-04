using System.IO;
using System.Web;
using System.Web.SessionState;
using NUnit.Framework;

namespace TestService.Tests
{
    [TestFixture]
    public class AuthTests
    {
        [SetUp]
        public void Setup()
        {
            WebServiceHelper.CreateHttpSessionTest();
        }

        [Test]
        public void Login()
        {
            Soap service = new Soap();
            bool result = service.Login("user1", "pass1");
            Assert.IsTrue(result);
            Assert.AreEqual("user1", service.UserName);
        }

    }
}
