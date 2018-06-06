using Microsoft.AspNet.Identity;
using NUnit.Framework;
using PortoGO.DB.Domain;
using PortoGO.DB.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Tests
{
    [TestFixture]
    public class ApplicationUserManagerTest
    {
        private UserManager<User> applicationUserManager;

        [SetUp]
        public void Init()
        {
            this.applicationUserManager = MocksFactory.ApplicationUserManager;
        }

        [Test]
        public void Should_Return_User()
        {
            var user = this.applicationUserManager.FindByName("admin");

            Assert.IsNotNull(user);
            Assert.AreEqual("admin", user.UserName);
        }
    }
}
