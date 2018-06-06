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
    public class LocationRepositoryTest
    {
        private ILocationRepository repository;
        private GpsCoordinate coords;

        [SetUp]
        public void Init()
        {
            this.repository = MocksFactory.LocationRepository;

            this.coords = new GpsCoordinate
            {
                Id = 129557542,
                Altitude = 0,
                Latitude = 41.1454754,
                Longitude = -8.6155655
            };
        }

        [Test]
        public void CountofLocation_Should_return_4()
        {
            int count = this.repository.Count();

            Assert.AreEqual(4, count);
        }

        [Test]
        public void Location_Should_Not_Be_Null()
        {
            var location = this.repository.Get(1);

            Assert.IsNotNull(location);
        }

        [Test]
        public void Location_Should_Be_The_Same()
        {
            var location = this.repository.Get(1);

            Assert.AreEqual("Torre dos Clérigos", location.Name);
        }

        [Test]
        public void Test_Location_Insert()
        {
            var newLocation = new Location("A", this.coords);

            this.repository.Insert(newLocation);
            
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
