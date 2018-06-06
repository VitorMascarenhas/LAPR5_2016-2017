using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public interface IUnitOfWork
    {
        ILocationRepository LocationRepository { get; }

        IPointOfInterestRepository PoiRepository { get; }

        IHashtagRepository HashtagRepository { get; }

        IVisitRepository VisitRepository { get; }

        IRoadRepository RoadRepository { get; }

        IUserAuditTrailRepository UserAuditTrailRepository { get; }

        IGpsCoordinateRepository GpsCoordinateRepository { get; }

        IRouteRepository RouteRepository { get; }

        void SaveChanges();
    }
}
