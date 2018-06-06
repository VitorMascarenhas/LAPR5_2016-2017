using AutoMapper;
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
    [AllowAnonymous]
    public class RoadController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public RoadController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Road
        [ResponseType(typeof(IEnumerable<RoadViewModel>))]
        public IEnumerable<RoadViewModel> Get()
        {
            IEnumerable<Road> data = unitOfWork.RoadRepository.GetAll("RoadCoordinates");

            IEnumerable<RoadViewModel> result = Mapper.Map<IEnumerable<RoadViewModel>>(data);

            return result;
        }

        [ResponseType(typeof(IEnumerable<RoadViewModel>))]
        public IEnumerable<RoadViewModel> Get(float fromLatitude, float fromLongitude, float toLatitude, float toLongitude)
        {
            IEnumerable<Road> data = unitOfWork.RoadRepository.Get(fromLatitude, fromLongitude, toLatitude, toLongitude);

            IEnumerable<RoadViewModel> result = Mapper.Map<IEnumerable<RoadViewModel>>(data);

            return result;
        }

        // GET: api/Road/5
        [ResponseType(typeof(RoadViewModel))]
        public IHttpActionResult Get(int id)
        {
            Road r = unitOfWork.RoadRepository.Find(x=>x.Id == id, null , "RoadCoordinates").FirstOrDefault();

            if (r == null)
            {
                return NotFound();
            }

            RoadViewModel result = Mapper.Map<RoadViewModel>(r);

            return Ok(result);
        }

    }
}
