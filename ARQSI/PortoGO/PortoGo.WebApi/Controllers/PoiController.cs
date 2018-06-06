using AutoMapper;
using Microsoft.AspNet.Identity;
using PortoGo.WebApi.Models;
using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace PortoGo.WebApi.Controllers
{
    [Authorize]
    public class PoiController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public PoiController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: api/Poi
        [ResponseType(typeof(IEnumerable<PoiViewModel>))]
        public IEnumerable<PoiViewModel> Get()
        {
            IEnumerable<PointOfInterest> data = unitOfWork.PoiRepository.GetAll("Location, Location.Coordinates,Hashtags");

            IEnumerable<PoiViewModel> result = Mapper.Map<IEnumerable<PoiViewModel>>(data);

            return result;
        }

        // GET: api/Poi/5
        [ResponseType(typeof(PoiViewModel))]
        public IHttpActionResult Get(int id)
        {
            PointOfInterest p = unitOfWork.PoiRepository.Find(x => x.Id == id, null, "Location,Location.Coordinates,Hashtags").FirstOrDefault();

            if (p == null)
            {
                return NotFound();
            }

            PoiViewModel result = Mapper.Map<PoiViewModel>(p);

            return Ok(result);
        }

        // POST: api/Poi
        [ResponseType(typeof(PoiViewModel))]
        public IHttpActionResult Post(PoiViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (viewModel == null)
            {
                return BadRequest(ModelState);
            }

            PointOfInterest p = new PointOfInterest(viewModel.Location.Id, viewModel.Description, viewModel.BusinessHours, viewModel.TimeTovisit, RequestContext.Principal.Identity.GetUserId());
            //p.UserId = RequestContext.Principal.Identity.GetUserId();

            unitOfWork.PoiRepository.Insert(p);
            unitOfWork.SaveChanges();

            viewModel.Id = p.Id;

            return CreatedAtRoute("DefaultApi", new { id = viewModel.Id }, viewModel);
        }

        // PUT: api/Poi
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, PoiViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            PointOfInterest p = Mapper.Map<PointOfInterest>(viewModel);

            try
            {
                unitOfWork.PoiRepository.Update(p);
                unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (unitOfWork.PoiRepository.Get(id) == null)
                    return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Poi/5
        [ResponseType(typeof(PoiViewModel))]
        public IHttpActionResult Delete(int id)
        {
            PointOfInterest p = unitOfWork.PoiRepository.Find(x => x.Id == id, null, "Location,Location.Coordinates").FirstOrDefault();

            if (p == null)
            {
                return NotFound();
            }

            PoiViewModel vm = Mapper.Map<PoiViewModel>(p);

            unitOfWork.PoiRepository.Delete(p);
            unitOfWork.SaveChanges();

            return Ok(vm);
        }

        [ResponseType(typeof(PoiViewModel))]
        [Route("api/poi/{id}/Approve/"), HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Approve(int id)
        {
            PointOfInterest p = unitOfWork.PoiRepository.Find(x=>x.Id == id, null, "Location,Location.Coordinates").FirstOrDefault();

            if (p == null)
            {
                return NotFound();
            }

            if (p.Status == Status.Approved)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            p.Approve();

            unitOfWork.PoiRepository.Update(p);
            unitOfWork.SaveChanges();

            PoiViewModel vm = Mapper.Map<PoiViewModel>(p);

            return Ok(vm);
        }

        [ResponseType(typeof(PoiViewModel))]
        [Route("api/poi/{id}/Reject/"), HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Reject(int id)
        {
            PointOfInterest p = unitOfWork.PoiRepository.Find(x => x.Id == id, null, "Location,Location.Coordinates").FirstOrDefault();

            if (p == null)
            {
                return NotFound();
            }

            if (p.Status != Status.Pending)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            p.Reject();

            unitOfWork.PoiRepository.Update(p);
            unitOfWork.SaveChanges();

            PoiViewModel vm = Mapper.Map<PoiViewModel>(p);

            return Ok(vm);
        }
    }
}
