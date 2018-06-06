using Moq;
using NUnit.Framework;
using PortoGo.WebApi.Controllers;
using PortoGo.WebApi.Models;
using PortoGO.DB.Repositories;
using PortoGO.DB.Tests.Mocks.Repositories;
using System.Net;
using System.Security.Principal;
using System.Web.Http.Results;

namespace PortoGo.WebApi.Tests.Controller
{
    [TestFixture]
    public class PoiControllerTest
    {
        private IUnitOfWork uow;
        private IIdentity mockIdentity;
        private PoiController controller;

        [SetUp]
        public void Init()
        {
            this.uow = new UnitOfWorkMock();

            this.mockIdentity = new Mock<IIdentity>().Object;

            this.controller = new PoiController(this.uow);

            MappingConfig.RegisterMaps();
        }

        [Test]
        public void PutPoi_ShouldFail_WhenDifferentID()
        {
            var vm = GetDemoPoi(4);

            var badresult = controller.Put(999, vm);

            Assert.IsInstanceOf<BadRequestResult>(badresult);
        }

        [Test]
        public void PutPoi_ShouldReturnStatusCode()
        {
            var item = GetDemoPoi(4);

            var result = controller.Put(item.Id, item) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void PostPoi_ShouldReturnSamePoi()
        {
            var item = GetDemoPoi(4);

            var result =
                controller.Post(item) as CreatedAtRouteNegotiatedContentResult<PoiViewModel>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Description, item.Description);
        }

        [Test]
        public void AprovePoi_ShouldChangeStatus()
        {
            var item = GetDemoPoi(4);

            var status = item.Status;

            var result = controller.Approve(item.Id) as OkNegotiatedContentResult<PoiViewModel>;

            Assert.IsNotNull(result);
            Assert.AreNotEqual(status, result.Content.Status);
        }

        [Test]
        public void RejectPoi_ShouldChangeStatus()
        {
            var item = GetDemoPoi(4);

            var status = item.Status;

            var result = controller.Reject(item.Id) as OkNegotiatedContentResult<PoiViewModel>;

            Assert.IsNotNull(result);
            Assert.AreNotEqual(status, result.Content.Status);
        }

        private PoiViewModel GetDemoPoi(int id)
        {
            var poi = this.uow.PoiRepository.Get(id);

            var vm = new PoiViewModel
            {
                Id = poi.Id,
                BusinessHours = poi.BusinessHours,
                Description = poi.Description,
                Location = null,
                Status = poi.Status,
                TimeTovisit = poi.TimeTovisit
            };

            return vm;
        }
    }
}
