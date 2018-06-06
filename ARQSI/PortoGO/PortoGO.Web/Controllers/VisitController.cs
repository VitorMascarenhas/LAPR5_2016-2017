using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using PortoGO.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Linq;
using System;
using System.Globalization;

namespace PortoGO.Web.Controllers
{
    public class VisitController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IIdentity identity;

        public VisitController(IUnitOfWork unitOfWork, IIdentity identity)
        {
            this.unitOfWork = unitOfWork;
            this.identity = identity;
        }

        // GET: Visit
        public ActionResult Index()
        {
            IEnumerable<VisitViewModel> result;
            if (Request.IsAuthenticated)
            {
                string userId = identity.GetUserId();
                IEnumerable<Visit> data = unitOfWork.VisitRepository.Find(x => x.User.Id == userId, null, "PointsOfInterests,StartLocation,StartLocation.Coordinates,User");
                result = Mapper.Map<IEnumerable<VisitViewModel>>(data);
                return View("Index_Auth", result);
            }
            else
            {
                IEnumerable<Visit> data = unitOfWork.VisitRepository.GetAll("PointsOfInterests,StartLocation,StartLocation.Coordinates");
                result = Mapper.Map<IEnumerable<VisitViewModel>>(data);
                return View("Index", result);
            }
        }

        public ActionResult Details(int id)
        {
            ViewBag.VisitId = id;
            Visit visit = unitOfWork.VisitRepository.Find(x => x.Id == id, null, "User,PointsOfInterests,Route, StartLocation,StartLocation.Coordinates, PointsOfInterests.Location.Coordinates,Route,Route.Nodes").FirstOrDefault();
            VisitViewModel result = Mapper.Map<VisitViewModel>(visit);

            result.Routes = Mapper.Map<IEnumerable<RouteViewModel>>(visit.Route).ToList();

            IEnumerable<PointOfInterest> pois = unitOfWork.PoiRepository.Find(x => x.Status == Status.Approved);

            IEnumerable<PoiViewModel> poiResult = Mapper.Map<IEnumerable<PoiViewModel>>(pois);

            result.PoisToAdd = poiResult.ToList();

            return View("Details", result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVisitViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var v = new Visit(vm.Name, Convert.ToDateTime(vm.StartDate), Convert.ToDateTime(vm.Enddate), vm.ReturnToStart, identity.GetUserId(), vm.Duration);

                string[] coords = vm.StartLocation.Split(',');
                double lat = Convert.ToDouble(coords[0], CultureInfo.InvariantCulture);
                double lon = Convert.ToDouble(coords[1], CultureInfo.InvariantCulture);

                Location location = unitOfWork.LocationRepository.Find(x => x.Coordinates.Latitude == lat && x.Coordinates.Longitude == lon).FirstOrDefault();
                if (location == null)
                {
                    location = CreateLocation(lat, lon);
                }

                v.StartLocation = location;

                unitOfWork.VisitRepository.Insert(v);
                unitOfWork.SaveChanges();

                return RedirectToAction("AddPoi", new { visitId = v.Id });
            }

            return View();
        }

        [Route("Visit/AddPoi/{visitId}")]
        public ActionResult AddPoi(int visitId)
        {
            IEnumerable<PointOfInterest> data = unitOfWork.PoiRepository.Find(x => x.Status == Status.Approved, null, "Location,User,Location.Coordinates");

            var result = Mapper.Map<IEnumerable<PoiViewModel>>(data);

            return View(result);
        }

        [Route("Visit/AddPoi/{visitId}")]
        [HttpPost]
        public ActionResult AddPoi(int visitId, int[] poi)
        {
            if (poi.Length == 0)
            {
                return View();
            }
            else
            {
                Visit v = unitOfWork.VisitRepository.Find(x => x.Id == visitId, null, "PointsOfInterests").FirstOrDefault();

                foreach (var item in poi)
                {
                    var p = unitOfWork.PoiRepository.Get(item);
                    v.PointsOfInterests.Add(p);
                }

                unitOfWork.VisitRepository.Update(v);
                unitOfWork.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            string userId = identity.GetUserId();
            var visit = unitOfWork.VisitRepository.Find(x => x.User.Id == userId && x.Id == id, null, "PointsOfInterests,StartLocation,StartLocation.Coordinates,User").FirstOrDefault();

            if (visit == null)
            {
                ViewBag.Id = id;

                return View("NotFound");
            }

            VisitViewModel result = Mapper.Map<VisitViewModel>(visit);

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                unitOfWork.VisitRepository.Delete(id);
                unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [HttpGet]
        [Route("Visit/DeletePoi/{visitId}/{id}")]
        public ActionResult DeletePoi(int visitId, int id)
        {
            var v = unitOfWork.VisitRepository.Find(x => x.Id == visitId, null, "PointsOfInterests").FirstOrDefault();

            var poi = unitOfWork.PoiRepository.Get(id);

            v.PointsOfInterests.Remove(poi);

            unitOfWork.VisitRepository.Update(v);
            unitOfWork.SaveChanges();

            return RedirectToAction("Details", new { id = visitId });

        }

        [HttpPost]
        public ActionResult AddPoi(int visitId, int poiToaddId)
        {
            var v = unitOfWork.VisitRepository.Find(x => x.Id == visitId, null, "PointsOfInterests").FirstOrDefault();

            var poi = unitOfWork.PoiRepository.Get(poiToaddId);

            v.PointsOfInterests.Add(poi);

            unitOfWork.VisitRepository.Update(v);
            unitOfWork.SaveChanges();

            return RedirectToAction("Details", new { id = visitId});
        }

        [HttpPost]
        public ActionResult DeleteRoute(int visitId, int routeId)
        {
            unitOfWork.RouteRepository.Delete(routeId);
            unitOfWork.SaveChanges();

            return RedirectToAction("Details", new { id = visitId });
        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/content/app.zip"));
            string fileName = "app.zip";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private Location CreateLocation(double latitude, double longitude)
        {
            var count = unitOfWork.GpsCoordinateRepository.Count();

            var c = new GpsCoordinate
            {
                Id = count + 100000000000,
                Latitude = latitude,
                Longitude = longitude
            };

            var location = new Location("", c);

            return location;
        }
    }
}