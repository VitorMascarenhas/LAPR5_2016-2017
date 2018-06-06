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
    public class GpsCoordinateRepositoryMock : IGpsCoordinateRepository
    {
        private IQueryable<GpsCoordinate> gps;

        public GpsCoordinateRepositoryMock()
        {

            Setup();
        }

        private void Setup()
        {
            var gps = new List<GpsCoordinate>();

            gps.Add(new GpsCoordinate { Id = 25620570, Altitude = 0, Latitude = 41.1529940, Longitude = -8.6120177 });
            gps.Add(new GpsCoordinate { Id = 25620653, Altitude = 0, Latitude = 41.1535970, Longitude = -8.6119957 });
            gps.Add(new GpsCoordinate { Id = 25620717, Altitude = 0, Latitude = 41.1479160, Longitude = -8.6145340 });
            gps.Add(new GpsCoordinate { Id = 25620737, Altitude = 0, Latitude = 41.1479508, Longitude = -8.6121496 });

            this.gps = gps.AsQueryable();
        }

        public int Count()
        {
            return gps.Count();
        }

        public int Count(Expression<Func<GpsCoordinate, bool>> predicate)
        {
            return gps.Count(predicate);
        }

        public void Delete(long id)
        {
            var tag = this.Get(id);

            this.Delete(tag);
        }

        public void Delete(GpsCoordinate entity)
        {
            var data = this.gps.ToList();

            data.Remove(entity);

            this.gps = data.AsQueryable();
        }

        public IEnumerable<GpsCoordinate> Find(Expression<Func<GpsCoordinate, bool>> predicate)
        {
            return this.gps.Where(predicate);
        }

        public IEnumerable<GpsCoordinate> Find(Expression<Func<GpsCoordinate, bool>> predicate, Func<IQueryable<GpsCoordinate>, IOrderedQueryable<GpsCoordinate>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public GpsCoordinate FirstOrDefault(Expression<Func<GpsCoordinate, bool>> predicate)
        {
            return this.gps.Where(predicate).FirstOrDefault();
        }

        public GpsCoordinate Get(long id)
        {
            var item = this.gps.Where(x => x.Id == id).FirstOrDefault();

            return item;
        }

        public IEnumerable<GpsCoordinate> GetAll()
        {
            return this.gps;
        }

        public IEnumerable<GpsCoordinate> GetAll(string includeProperties = "")
        {
            return this.gps;
        }

        public GpsCoordinate Insert(GpsCoordinate entity)
        {
            var data = this.gps.ToList();

            entity.Id = data.Count + 1;

            data.Add(entity);

            this.gps = data.AsQueryable();

            return entity;
        }

        public GpsCoordinate InsertOrUpdate(GpsCoordinate entity)
        {
            throw new NotImplementedException();
        }

        public void Update(GpsCoordinate entity)
        {
            var oldPoi = this.Get(entity.Id);

            var data = this.gps.ToList();

            data.Remove(oldPoi);

            data.Add(entity);

            this.gps = data.AsQueryable();
        }
    }
}
