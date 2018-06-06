using NUnit.Framework;
using PortoGO.DB.Repositories;
using System.Security.Principal;
using PortoGO.Web.Controllers;
using PortoGO.Web;
using PortoGO.DB.Tests.Mocks.Repositories;
using PortoGO.DB.Tests.Mocks.Identity;
using Moq;
using System.Web.Mvc;

namespace PortoGo.Web.Tests.Controller
{
    [TestFixture]
    public class PoiControllerTest
    {
        private PoiController controller;
        private IUnitOfWork uow;
        private IIdentity mockIdentity;
        
        [SetUp]
        public void Init()
        {
            this.uow = new UnitOfWorkMock();

            this.mockIdentity = new Mock<IIdentity>().Object;

            this.controller = new PoiController(this.uow, mockIdentity);

            MappingConfig.RegisterMaps();
        }
        
        [Test]
        public void TestIndexView()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void TestDetailsView()
        {
            var result = controller.Details(2) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }

        [Test]
        public void TestDelete()
        {

        }
    }
}