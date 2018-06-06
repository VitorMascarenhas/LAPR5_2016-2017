using AutoMapper;
using PortoGo.WebApi.Models;
using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PortoGo.WebApi.Controllers
{
    [Authorize]
    public class RouteController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public RouteController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Route
        [ResponseType(typeof(RouteViewModel))]
        [Route("api/Route/visit/{visitId}")]
        public IEnumerable<RouteViewModel> GetRoutesOfVisit(int visitId)
        {
            var data = unitOfWork.RouteRepository.Find(x => x.VisitId == visitId, null, "PointOfInterest,Nodes,PointOfInterest.Location");

            IEnumerable<RouteViewModel> result = Mapper.Map<IEnumerable<RouteViewModel>>(data);

            return result;
        }

        // GET: api/Route/5
        public IHttpActionResult Get(int id)
        {
            var data = unitOfWork.RouteRepository.Find(x => x.Id == id, null, "PointOfInterest,Nodes,PointOfInterest.Location").FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            RouteViewModel result = Mapper.Map<RouteViewModel>(data);

            return Ok(result);
        }

        // POST: api/Route
        [ResponseType(typeof(RouteViewModel))]
        public IHttpActionResult Post(CreateRouteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (viewModel == null)
            {
                return BadRequest(ModelState);
            }

            var route = new Route
            {
                VisitId = viewModel.VisitId,
                Hour = viewModel.Hour,
                Order = viewModel.Order,
                PointOfInterestId = viewModel.PoiId,
                RunTime = viewModel.RunTime

            };

            unitOfWork.RouteRepository.Insert(route);
            unitOfWork.SaveChanges();

            viewModel.Id = route.Id;

            return CreatedAtRoute("DefaultApi", new { id = viewModel.Id }, viewModel);
        }

        // PUT: api/Route/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, CreateRouteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            Route p = unitOfWork.RouteRepository.Get(id);
            p.Order = viewModel.Order;
            p.RunTime = viewModel.RunTime;
            p.VisitId = viewModel.VisitId;
            p.PointOfInterestId = viewModel.PoiId;
            p.Hour = viewModel.Hour;

            try
            {
                unitOfWork.RouteRepository.Update(p);
                unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.RouteRepository.Get(id) == null)
                    return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Route/5
        [ResponseType(typeof(RouteViewModel))]
        public IHttpActionResult Delete(int id)
        {
            Route p = unitOfWork.RouteRepository.Get(id);

            if (p == null)
            {
                return NotFound();
            }

            RouteViewModel vm = Mapper.Map<RouteViewModel>(p);

            unitOfWork.RouteRepository.Delete(p);
            unitOfWork.SaveChanges();

            return Ok(vm);
        }
    }
}
