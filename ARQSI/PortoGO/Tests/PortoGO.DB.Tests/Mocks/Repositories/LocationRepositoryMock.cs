using Moq;
using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PortoGO.DB.Tests.Mocks.Repositories
{
    public class LocationRepositoryMock : ILocationRepository
    {
        private IQueryable<Location> locations;
        private GpsCoordinate coords;

        public LocationRepositoryMock()
        {
            coords = new GpsCoordinate
            {
                Id = 129557542,
                Altitude = 0,
                Latitude = 41.1454754,
                Longitude = -8.6155655
            };

            this.locations = SetupLocations();
        }

        private IQueryable<Location> SetupLocations()
        {
            var l = new List<Location>();

            l.Add(new Location("Torre dos Clérigos", coords) { Id = 1 });
            l.Add(new Location("ISEP", coords) { Id = 2 });
            l.Add(new Location("Estação de São Bento", coords) { Id = 3 });
            l.Add(new Location("Estação de Campanhã", coords) { Id = 4 });

            return l.AsQueryable();
        }

        public int Count()
        {
            return this.locations.Count();
        }

        public int Count(Expression<Func<Location, bool>> predicate)
        {
            return this.locations.Count(predicate);
        }

        public void Delete(int id)
        {
            var location = this.Get(id);

            this.Delete(location);
        }

        public void Delete(Location entity)
        {
            var data = this.locations.ToList();

            data.Remove(entity);

            this.locations = data.AsQueryable();
        }

        public IEnumerable<Location> Find(Expression<Func<Location, bool>> predicate)
        {
            return this.locations.Where(predicate);
        }

        public IEnumerable<Location> Find(Expression<Func<Location, bool>> predicate, Func<IQueryable<Location>, IOrderedQueryable<Location>> orderBy = null, string includeProperties = "")
        {
            return this.locations.Where(predicate);
        }

        public Location FirstOrDefault(Expression<Func<Location, bool>> predicate)
        {
            return this.locations.Where(predicate).FirstOrDefault();
        }

        public Location Get(int id)
        {
            var location = this.locations.Where(x => x.Id == id).FirstOrDefault();

            return location;
        }

        public IEnumerable<Location> GetAll()
        {
            return this.locations;
        }

        public IEnumerable<Location> GetAll(string includeProperties = "")
        {
            return this.locations;
        }

        public Location Insert(Location entity)
        {
            var data = this.locations.ToList();

            entity.Id = data.Count + 1;

            data.Add(entity);

            this.locations = data.AsQueryable();

            return entity;
        }

        public Location InsertOrUpdate(Location entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Location entity)
        {
            throw new NotImplementedException();
        }
    }
}
