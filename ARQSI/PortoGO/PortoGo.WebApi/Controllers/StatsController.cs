using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PortoGo.WebApi.Models;
using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PortoGo.WebApi.Controllers
{
    [Authorize]
    public class StatsController : ApiController
    {
        private ApplicationUserManager userManager;
        private readonly IUnitOfWork uow;

        public StatsController(IUnitOfWork uow)
        {
            this.uow = uow;
            
        }

        [ResponseType(typeof(StatsViewModel))]
        public IHttpActionResult Get()
        {
            this.userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var countOfUsers = userManager.Users.Count();
            var countOfRejectedPoi = this.uow.PoiRepository.Find(x => x.Status == PortoGO.DB.Domain.Status.Rejected).Count();
            var countOfApprovedPoi = this.uow.PoiRepository.Find(x => x.Status == PortoGO.DB.Domain.Status.Approved).Count();
            var countOfPendingPoi = this.uow.PoiRepository.Find(x => x.Status == PortoGO.DB.Domain.Status.Pending).Count();
            var countOfPoi = this.uow.PoiRepository.Count();

            var stats = new StatsViewModel
            {
                CountOfApprovedPoi = countOfApprovedPoi,
                CountOfPendingPoi = countOfPendingPoi,
                CountOfPoi = countOfPoi,
                CountOfRejectedPoi = countOfRejectedPoi,
                CountOfUsers = countOfUsers
            };

            return Ok(stats);
        }
    }
}
