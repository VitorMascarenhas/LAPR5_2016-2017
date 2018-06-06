using PortoGO.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Tests.Mocks.Repositories
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        public IGpsCoordinateRepository GpsCoordinateRepository
        {
            get
            {
                return MocksFactory.GpsCoordinateRepository;
            }
        }

        public IHashtagRepository HashtagRepository
        {
            get
            {
                return MocksFactory.HashtagRepository;
            }
        }

        public ILocationRepository LocationRepository
        {
            get
            {
                return MocksFactory.LocationRepository;
            }
        }

        public IPointOfInterestRepository PoiRepository
        {
            get
            {
                return MocksFactory.PoiRepository;
            }
        }

        public IRoadRepository RoadRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRouteRepository RouteRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IUserAuditTrailRepository UserAuditTrailRepository
        {
            get
            {
                return MocksFactory.UserAuditTrailRepository;
            }
        }

        public IVisitRepository VisitRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void SaveChanges()
        {
            
        }
    }
}
