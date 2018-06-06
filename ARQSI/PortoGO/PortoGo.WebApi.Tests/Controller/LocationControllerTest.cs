using Moq;
using NUnit.Framework;
using PortoGo.WebApi.Controllers;
using PortoGo.WebApi.Models;
using PortoGO.DB.Repositories;
using PortoGO.DB.Tests.Mocks.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace PortoGo.WebApi.Tests.Controller
{
    [TestFixture]
    public class LocationControllerTest
    {
        private IUnitOfWork uow;
        private IIdentity mockIdentity;
        private LocationController controller;

        [SetUp]
        public void Init()
        {
            this.uow = new UnitOfWorkMock();

            this.mockIdentity = new Mock<IIdentity>().Object;

            this.controller = new LocationController(this.uow);

            MappingConfig.RegisterMaps();
        }

        [Test]
        public void GetAll_ShouldReturn_Data()
        {
            IEnumerable<Models.LocationViewModel> result = this.controller.Get();

            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public void Get_ShouldReturn_SingleLocation()
        {
            var result = this.controller.Get(1) as OkNegotiatedContentResult<LocationViewModel>;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkNegotiatedContentResult<LocationViewModel>>(result);
            Assert.AreEqual("Torre dos Clérigos", result.Content.Name);
            Assert.AreEqual(1, result.Content.Id);
        }

        [Test]
        public void Get_InvalidLocation_ShouldReturn_NotFound()
        {
            var result = this.controller.Get(99999) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
