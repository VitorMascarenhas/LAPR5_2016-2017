using AutoMapper;
using Microsoft.AspNet.Identity;
using PortoGo.WebApi.Models;
using PortoGO.DB.Domain;
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
    public class VisitController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public VisitController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: api/Visit
        [ResponseType(typeof(IEnumerable<VisitViewModel>))]
        public IEnumerable<VisitViewModel> Get()
        {
            IEnumerable<PortoGO.DB.Domain.Visit> data = unitOfWork.VisitRepository.GetAll("User,PointsOfInterests,Route,StartLocation,StartLocation.Coordinates,PointsOfInterests.Location,PointsOfInterests.Location.Coordinates,Route.Nodes,Route.PointOfInterest");
            IEnumerable<VisitViewModel> result = Mapper.Map<IEnumerable<VisitViewModel>>(data);

            return result;
        }

        [ResponseType(typeof(VisitViewModel))]
        public IHttpActionResult Get(int id)
        {
            Visit visit = unitOfWork.VisitRepository.Find(x => x.Id == id, null, "User,PointsOfInterests,Route,StartLocation,StartLocation.Coordinates,PointsOfInterests.Location,PointsOfInterests.Location.Coordinates,Route.Nodes,Route.PointOfInterest").FirstOrDefault();

            if (visit == null)
            {
                return NotFound();
            }

            VisitViewModel result = Mapper.Map<VisitViewModel>(visit);

            return Ok(result);
        }

        [Route("api/visit/user/")]
        [ResponseType(typeof(VisitViewModel))]
        public IHttpActionResult GetVisitForUser()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var visits = unitOfWork.VisitRepository.Find(x => x.User.Id == userId, null, "User,PointsOfInterests,Route,StartLocation,StartLocation.Coordinates,PointsOfInterests.Location,PointsOfInterests.Location.Coordinates,Route.Nodes,Route.PointOfInterest");

            if (!visits.Any())
            {
                return NotFound();
            }

            var result = Mapper.Map<IEnumerable<VisitViewModel>>(visits);

            return Ok(result);
        }


        //[Route("api/visit/{visitId}/route/road/{roadId}")]
        //[HttpDelete]
        //[ResponseType(typeof(VisitViewModel))]
        //public IHttpActionResult RemoveRoad(int visitId, int roadId)
        //{
        //    Visit visit = unitOfWork.VisitRepository.Find(x => x.Id == visitId, null, "Route").FirstOrDefault();

        //    if (visit == null)
        //    {
        //        return NotFound();
        //    }

        //    if(visit.Route != null)
        //    {
        //        IEnumerable<Road> roads = visit.Route.Roads.Where(x => x.Id == roadId);
        //        if (!roads.Any())
        //        {
        //            return NotFound();
        //        }

        //        visit.Route.Roads.Remove(roads.FirstOrDefault());
        //        unitOfWork.VisitRepository.Update(visit);
        //        unitOfWork.SaveChanges();
        //    }

        //    VisitViewModel vm = Mapper.Map<VisitViewModel>(visit);

        //    return Ok(vm);
        //}

        //[Route("api/visit/{visitId}/route/road/{roadId}")]
        //[HttpPost]
        //[ResponseType(typeof(VisitViewModel))]
        //public IHttpActionResult PostRoad(int visitId, int roadId)
        //{
        //    Visit visit = unitOfWork.VisitRepository.Find(x => x.Id == visitId, null, "Route").FirstOrDefault();

        //    if (visit == null)
        //    {
        //        return NotFound();
        //    }

        //    if(visit.Route != null)
        //    {
        //        IEnumerable<Road> roads = visit.Route.Roads.Where(x => x.Id == roadId);
        //        if (roads.Any())
        //        {
        //            return StatusCode(HttpStatusCode.Conflict);
        //        }
        //    }
        //    else
        //    {
        //        visit.Route = new Route();
        //    }

        //    Road road = unitOfWork.RoadRepository.Get(roadId);

        //    visit.Route.Roads.Add(road);
        //    unitOfWork.VisitRepository.Update(visit);
        //    unitOfWork.SaveChanges();

        //    VisitViewModel vm = Mapper.Map<VisitViewModel>(visit);

        //    return Ok(vm);
        //}

        //[Route("api/visit/{visitId}/route/")]
        //[HttpDelete]
        //[ResponseType(typeof(VisitViewModel))]
        //public IHttpActionResult DeleteRoute(int visitId)
        //{
        //    Visit visit = unitOfWork.VisitRepository.Find(x => x.Id == visitId, null, "Route").FirstOrDefault();

        //    if (visit == null)
        //    {
        //        return NotFound();
        //    }

        //    unitOfWork.RouteRepository.Delete(visit.Route);
        //    unitOfWork.SaveChanges();

        //    VisitViewModel vm = Mapper.Map<VisitViewModel>(visit);
        //    vm.Route = null;

        //    return Ok(vm);
        //}

        //[Route("api/visit/{visitId}/route")]
        //public IHttpActionResult PostRoute(int visitId, int[] roadIds)
        //{
        //    if (roadIds.Length == 0)
        //    {
        //        return BadRequest("Road Id's are mandatory");
        //    }

        //    Visit visit = unitOfWork.VisitRepository.Find(x => x.Id == visitId, null, "Route").FirstOrDefault();

        //    if(visit.Route != null)
        //    {
        //        return Ok("This visit already has a route. No work was done.");
        //    }

        //    var route = new Route(visitId);
        //    foreach (var item in roadIds)
        //    {
        //        var road = unitOfWork.RoadRepository.Get(item);
        //        if(road!= null)
        //        {
        //            route.Roads.Add(road);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }

        //    unitOfWork.RouteRepository.Insert(route);
        //    unitOfWork.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = visitId }, roadIds);
        //}
    }
}
