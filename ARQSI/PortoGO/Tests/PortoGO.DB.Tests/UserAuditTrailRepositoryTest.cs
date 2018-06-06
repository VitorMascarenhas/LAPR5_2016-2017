using NUnit.Framework;
using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using PortoGO.DB.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Tests
{
    [TestFixture]
    public class UserAuditTrailRepositoryTest
    {
        private IUserAuditTrailRepository repository;

        private User user;

        [SetUp]
        public void Init()
        {
            this.repository = MocksFactory.UserAuditTrailRepository;

            user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Email = "admin@google.com",
                DisplayName = "Administrator"
            };
        }

        [Test]
        public void Test_LogAccountCreation()
        {
            this.repository.LogAccountCreation(user);

            var count = this.repository.Count(x => x.UserName == user.UserName && x.Type == UserAuditTrailType.Add);

            Assert.IsTrue(count > 0);
        }

        [Test]
        public void Test_LogAccountDeletion()
        {
            this.repository.LogAccountDeletion(user);

            var count = this.repository.Count(x => x.UserName == user.UserName && x.Type == UserAuditTrailType.Remove);

            Assert.IsTrue(count > 0);
        }
    }
}
