using Microsoft.AspNet.Identity;
using PortoGO.DB.Domain;
using PortoGO.DB.Repositories;
using PortoGO.DB.Tests.Mocks.Identity;
using PortoGO.DB.Tests.Mocks.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Tests.Mocks
{
    public class MocksFactory
    {
        public static ILocationRepository LocationRepository
        {
            get
            {
                return new LocationRepositoryMock();
            }
        }

        public static UserManager<User> ApplicationUserManager
        {
            get
            {
                return UserManagerMock.Create();
            }
        }

        public static IPointOfInterestRepository PoiRepository
        {
            get
            {
                return new PointOfInterestRepositoryMock();
            }
        }

        public static IHashtagRepository HashtagRepository
        {
            get
            {
                return new HashtagRepositoryMock();
            }
        }

        public static IUserAuditTrailRepository UserAuditTrailRepository
        {
            get
            {
                return new UserAuditTrailRepositoryMock();
            }
        }

        public static IGpsCoordinateRepository GpsCoordinateRepository
        {
            get
            {
                return new GpsCoordinateRepositoryMock();
            }
        }
    }
}
