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
    public class LocationController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public LocationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Location
        [ResponseType(typeof(IEnumerable<LocationViewModel>))]
        public IEnumerable<LocationViewModel> Get()
        {
            IEnumerable<Location> data = unitOfWork.LocationRepository.GetAll("Coordinates");

            IEnumerable<LocationViewModel> result = Mapper.Map<IEnumerable<LocationViewModel>>(data);

            return result;
        }

        // GET: api/Location/5
        [ResponseType(typeof(LocationViewModel))]
        public IHttpActionResult Get(int id)
        {
            Location location = unitOfWork.LocationRepository.Find(x => x.Id == id, null, "Coordinates").FirstOrDefault();

            if (location == null)
            {
                return NotFound();
            }

            LocationViewModel result = Mapper.Map<LocationViewModel>(location);

            return Ok(result);
        }
    }
}
