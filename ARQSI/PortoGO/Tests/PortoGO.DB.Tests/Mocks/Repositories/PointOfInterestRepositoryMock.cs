using Moq;
using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortoGO.DB.Domain;
using System.Linq.Expressions;

namespace PortoGO.DB.Tests.Mocks.Repositories
{
    public class PointOfInterestRepositoryMock : IPointOfInterestRepository
    {
        private IQueryable<PointOfInterest> pointsOfInterest;

        private readonly Location location;

        private GpsCoordinate coords;

        public PointOfInterestRepositoryMock()
        {
            coords = new GpsCoordinate
            {
                Id = 129557542,
                Altitude = 0,
                Latitude = 41.1454754,
                Longitude = -8.6155655
            };

            location = new Location("Torre dos Clérigos", coords);

            Setup();
        }

        private void Setup()
        {
            var pois = new List<PointOfInterest>();

            var user = new User()
            {
                Id ="1",
                UserName = "admin",
                Email = "admin@google.com",
                DisplayName = "Administrator"
            };

            var b = new BusinessHours { FromHour = new TimeSpan(9, 0, 0), ToHour = new TimeSpan(17, 59, 59) };

            var p = new PointOfInterest(location, "Teste 1", b, 1, user);
            p.Id = 1;
            p.Approve();

            pois.Add(p);
            pois.Add(new PointOfInterest(location, "Teste 2", b, 1, user) { Id = 2 });

            p = new PointOfInterest(location, "Teste 3", b, 1, user);
            p.Id = 3;
            p.Approve();
            pois.Add(p);

            pois.Add(new PointOfInterest(location, "Teste 4", b, 1, user) { Id = 4 });

            this.pointsOfInterest = pois.AsQueryable();
        }

        public int Count()
        {
            return this.pointsOfInterest.Count();
        }

        public int Count(Expression<Func<PointOfInterest, bool>> predicate)
        {
            return this.pointsOfInterest.Count(predicate);
        }

        public void Delete(int id)
        {
            var poi = this.Get(id);

            this.Delete(poi);
        }

        public void Delete(PointOfInterest entity)
        {
            var data = this.pointsOfInterest.ToList();

            data.Remove(entity);

            this.pointsOfInterest = data.AsQueryable();
        }

        public IEnumerable<PointOfInterest> Find(Expression<Func<PointOfInterest, bool>> predicate)
        {
            return this.pointsOfInterest.Where(predicate);
        }

        public IEnumerable<PointOfInterest> Find(Expression<Func<PointOfInterest, bool>> predicate, Func<IQueryable<PointOfInterest>, IOrderedQueryable<PointOfInterest>> orderBy = null, string includeProperties = "")
        {
            return this.pointsOfInterest.Where(predicate);
        }

        public PointOfInterest FirstOrDefault(Expression<Func<PointOfInterest, bool>> predicate)
        {
            return this.Find(predicate).FirstOrDefault();
        }

        public PointOfInterest Get(int id)
        {
            var poi = this.pointsOfInterest.Where(x => x.Id == id).FirstOrDefault();

            return poi;
        }

        public IEnumerable<PointOfInterest> GetAll()
        {
            return this.pointsOfInterest;
        }

        public IEnumerable<PointOfInterest> GetAll(string includeProperties = "")
        {
            return this.pointsOfInterest;
        }

        public PointOfInterest Insert(PointOfInterest entity)
        {
            var data = this.pointsOfInterest.ToList();

            entity.Id = data.Count + 1;

            data.Add(entity);

            this.pointsOfInterest = data.AsQueryable();

            return entity;
        }

        public PointOfInterest InsertOrUpdate(PointOfInterest entity)
        {
            throw new NotImplementedException();
        }

        public void Update(PointOfInterest entity)
        {
            var oldPoi = this.Get(entity.Id);

            var data = this.pointsOfInterest.ToList();

            data.Remove(oldPoi);

            data.Add(entity);

            this.pointsOfInterest = data.AsQueryable();
        }

        public IEnumerable<PointOfInterest> GetUserPointsOfInterest(string userId, bool includeHashtags = false)
        {
            if(userId == "1")
            {
                return this.GetAll();
            }
            else
            {
                return null;
            }
        }
    }
}
