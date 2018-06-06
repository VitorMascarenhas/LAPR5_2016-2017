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
    public class HashtagRepositoryMock : IHashtagRepository
    {
        private IQueryable<Hashtag> hashtags;

        private readonly PointOfInterest poi;
        private readonly Location location;
        private readonly User user;
        private GpsCoordinate coords;

        public HashtagRepositoryMock()
        {
            var b = new BusinessHours { FromHour = new TimeSpan(9, 0, 0), ToHour = new TimeSpan(17, 59, 58) };
            user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Email = "admin@google.com",
                DisplayName = "Administrator"
            };

            coords = new GpsCoordinate
            {
                Id = 129557542,
                Altitude = 0,
                Latitude = 41.1454754,
                Longitude = -8.6155655
            };

            location = new Location("Torre dos Clérigos", coords);
            poi = new PointOfInterest(location, "Mock POI", b, 3, user);

            Setup();
        }

        private void Setup()
        {
            var tags = new List<Hashtag>();

            for (int i = 1; i < 5; i++)
            {
                tags.Add(new Hashtag(string.Format("Tag-{0}", i), poi, user) { Id = i});
            }

            this.hashtags = tags.AsQueryable();
        }

        public int Count()
        {
            return this.hashtags.Count();
        }

        public int Count(Expression<Func<Hashtag, bool>> predicate)
        {
            return this.hashtags.Count(predicate);
        }

        public void Delete(int id)
        {
            var tag = this.Get(id);

            this.Delete(tag);
        }

        public void Delete(Hashtag entity)
        {
            var data = this.hashtags.ToList();

            data.Remove(entity);

            this.hashtags = data.AsQueryable();
        }

        public IEnumerable<Hashtag> Find(Expression<Func<Hashtag, bool>> predicate)
        {
            return this.hashtags.Where(predicate);
        }

        public IEnumerable<Hashtag> Find(Expression<Func<Hashtag, bool>> predicate, Func<IQueryable<Hashtag>, IOrderedQueryable<Hashtag>> orderBy = null, string includeProperties = "")
        {
            return this.hashtags.Where(predicate);
        }

        public Hashtag FirstOrDefault(Expression<Func<Hashtag, bool>> predicate)
        {
            return this.hashtags.Where(predicate).FirstOrDefault();
        }

        public Hashtag Get(int id)
        {
            var item = this.hashtags.Where(x => x.Id == id).FirstOrDefault();

            return item;
        }

        public IEnumerable<Hashtag> GetAll()
        {
            return this.hashtags;
        }

        public IEnumerable<Hashtag> GetAll(string includeProperties = "")
        {
            return this.hashtags;
        }

        public Hashtag Insert(Hashtag entity)
        {
            var data = this.hashtags.ToList();

            entity.Id = data.Count + 1;

            data.Add(entity);

            this.hashtags = data.AsQueryable();

            return entity;
        }

        public Hashtag InsertOrUpdate(Hashtag entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Hashtag entity)
        {
            var oldPoi = this.Get(entity.Id);

            var data = this.hashtags.ToList();

            data.Remove(oldPoi);

            data.Add(entity);

            this.hashtags = data.AsQueryable();
        }
    }
}
