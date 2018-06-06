using NUnit.Framework;
using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using PortoGO.DB.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Tests
{
    [TestFixture]
    public class HashtagRepositoryTest
    {
        private IHashtagRepository repository;

        private User user;
        private PointOfInterest poi;
        private Location location;
        private GpsCoordinate coords;

        [SetUp]
        public void Init()
        {
            this.repository = MocksFactory.HashtagRepository;

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
            var b = new BusinessHours { FromHour = new TimeSpan(9, 0, 0), ToHour = new TimeSpan(17, 59, 58) };
            poi = new PointOfInterest(location, "Mock POI", b, 3, user);
        }

        [Test]
        public void CountofHashtag_Should_return_4()
        {
            int count = this.repository.Count();

            Assert.AreEqual(4, count);
        }

        [Test]
        public void Hashtag_Should_Not_Be_Null()
        {
            var tag = this.repository.Get(1);

            Assert.IsNotNull(tag);
        }

        [Test]
        public void Hashtag_Should_Be_The_Same()
        {
            var tag = this.repository.Get(1);

            Assert.AreEqual("Tag-1", tag.Tag);
        }

        [Test]
        public void Test_Hashtag_Insert()
        {
            var newTag = new Hashtag("Mock Tag", poi, user);

            this.repository.Insert(newTag);

            Assert.AreEqual(5, repository.Count());
        }

        [Test]
        public void Test_Location_Delete()
        {
            var item = this.repository.Get(1);

            this.repository.Delete(item);

            Assert.Null(this.repository.Get(1));
        }
    }
}
