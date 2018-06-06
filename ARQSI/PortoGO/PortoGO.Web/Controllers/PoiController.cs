using System.Collections.Generic;
using System.Web.Mvc;
using PortoGO.DB.Repositories;
using PortoGO.DB.Domain;
using PortoGO.Web.ViewModels;
using AutoMapper;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNet.Identity;


namespace PortoGO.Web.Controllers
{
    [Authorize]
    public class PoiController : Controller
    {
        private readonly IIdentity identity;
        private readonly IUnitOfWork unitOfWork;

        public PoiController(IUnitOfWork unitOfWork, IIdentity identity)
        {
            this.unitOfWork = unitOfWork;
            this.identity = identity;
        }

        // GET: Poi
        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<PoiViewModel> result;
            if (!Request.IsAuthenticated)
            {
                IEnumerable<PointOfInterest> data = unitOfWork.PoiRepository.Find(x => x.Status == Status.Approved, null, "Location,User,Location.Coordinates");
                result = Mapper.Map<IEnumerable<PoiViewModel>>(data);

            }
            else
            {
                var data = unitOfWork.PoiRepository.GetAll("Location,User,Hashtags,Location.Coordinates");
                result = Mapper.Map<IEnumerable<PoiViewModel>>(data);

                foreach (var item in result)
                {
                    var poi = data.FirstOrDefault(x => x.Id == item.Id);

                    item.IsOwner = poi.IsOwner(identity.GetUserId());
                }
            }

            return View("Index", result);

        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            PointOfInterest poi = unitOfWork.PoiRepository.Find(x => x.Id == id, null, "Location,User, Location.Coordinates").FirstOrDefault();

            if (poi == null || (poi.Status != Status.Approved && !Request.IsAuthenticated))
            {
                return View("NotFound");
            }

            IEnumerable<Hashtag> hashtags;

            PoiViewModel result = Mapper.Map<PoiViewModel>(poi);

            if (Request.IsAuthenticated)
            {
                var userId = identity.GetUserId();
                result.IsOwner = poi.IsOwner(userId);
                //so vamos mostrar as hashtags criadas pelo user
                hashtags = unitOfWork.HashtagRepository.Find(x => x.PointOfInterestId == poi.Id && x.UserId == userId);
            }
            else
            {
                hashtags = unitOfWork.HashtagRepository.GetAll();
            }
            
            List<HashtagViewModel> tagResult = Mapper.Map<List<HashtagViewModel>>(hashtags);

            result.Hashtags = tagResult;

            return View("Details", result);
        }

        public ActionResult Create()
        {
            IEnumerable<Location> data = unitOfWork.LocationRepository.GetAll();
            var result = Mapper.Map<IEnumerable<LocationViewModel>>(data);

            var vm = new CreatePoiViewModel();
            vm.Locations = result.ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CreatePoiViewModel poi)
        {
            if (ModelState.IsValid)
            {
                var b = new BusinessHours { FromHour = poi.BusinessHoursFromHour.Value, ToHour = poi.BusinessHoursToHour.Value };

                var newPoi = new PointOfInterest(poi.LocationId, poi.Description, b, poi.TimeTovisit.Value, identity.GetUserId());


                if (!string.IsNullOrEmpty(poi.Hashtags))
                {
                    var hashtags = poi.Hashtags.Split(',');

                    foreach (var tag in hashtags)
                    {
                        newPoi.Hashtags.Add(new Hashtag { UserId = newPoi.UserId, Tag = tag });
                    }
                }

                unitOfWork.PoiRepository.Insert(newPoi);
                unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(poi);
        }

        public ActionResult Delete(int id)
        {
            PointOfInterest poi = unitOfWork.PoiRepository.Find(x => x.Id == id, null, "Location,User, Location.Coordinates").FirstOrDefault();

            if (poi == null)
            {
                ViewBag.Id = id;

                return View("NotFound");
            }

            if (!poi.IsOwner(identity.GetUserId()))
            {
                return View("NotFound");
            }

            PoiViewModel result = Mapper.Map<PoiViewModel>(poi);

            return View(result);
        }

        // POST: Poi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                unitOfWork.PoiRepository.Delete(id);
                unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            PointOfInterest poi = unitOfWork.PoiRepository.Find(x => x.Id == id, null, "Location,User, Location.Coordinates").FirstOrDefault();

            if (poi.UserId != identity.GetUserId())
            {
                return View("NotFound");
            }

            if (poi == null)
            {
                return View("NotFound");
            }

            var userId = identity.GetUserId();
            //so vamos mostrar as hashtags criadas pelo user
            var hashtags = unitOfWork.HashtagRepository.Find(x => x.PointOfInterestId == poi.Id && x.UserId == userId);

            PoiViewModel result = Mapper.Map<PoiViewModel>(poi);
            List<HashtagViewModel> tagResult = Mapper.Map<List<HashtagViewModel>>(hashtags);

            result.Hashtags = tagResult;

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PoiViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                PointOfInterest poi = unitOfWork.PoiRepository.Find(x => x.Id == viewModel.Id, null, "Hashtags").FirstOrDefault();

                string userId = identity.GetUserId();

                var entityTags = poi.Hashtags.Where(x => x.UserId == userId).ToList();

                foreach (var item in entityTags)
                {
                    unitOfWork.HashtagRepository.Delete(item);
                }

                if (!string.IsNullOrEmpty(viewModel.HashtagString))
                {
                    string[] vmTags = viewModel.HashtagString.Split(',');

                    foreach (var tag in vmTags)
                    {
                        unitOfWork.HashtagRepository.Insert(new Hashtag(tag, poi.Id, userId));
                    }
                }

                poi.Description = viewModel.Description;

                if (viewModel.BusinessHoursFromHour.HasValue) poi.BusinessHours.FromHour = viewModel.BusinessHoursFromHour.Value;
                if (viewModel.BusinessHoursToHour.HasValue)  poi.BusinessHours.ToHour = viewModel.BusinessHoursToHour.Value;

                //TODO: alterar a location
                if(viewModel.TimeTovisit.HasValue) poi.TimeTovisit = viewModel.TimeTovisit.Value;

                unitOfWork.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}