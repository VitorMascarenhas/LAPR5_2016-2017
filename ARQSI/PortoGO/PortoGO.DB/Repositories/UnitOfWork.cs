using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext context;

        private ILocationRepository locationRepository;
        public ILocationRepository LocationRepository
        {
            get
            {
                if(locationRepository == null)
                {
                    locationRepository = new LocationRepository(this.context);
                }

                return locationRepository;
            }
        }

        private IPointOfInterestRepository poiRepository;
        public IPointOfInterestRepository PoiRepository
        {
            get
            {
                if(poiRepository == null)
                {
                    poiRepository = new PointOfInterestRepository(this.context);
                }

                return poiRepository;
            }
        }

        private IHashtagRepository hashtagRepository;
        public IHashtagRepository HashtagRepository
        {
            get
            {
                if(hashtagRepository == null)
                {
                    hashtagRepository = new HashtagRepository(this.context);
                }

                return hashtagRepository;
            }
        }

        private IVisitRepository visitRepository;
        public IVisitRepository VisitRepository
        {
            get
            {
                if(visitRepository == null)
                {
                    visitRepository = new VisitRepository(this.context);
                }

                return visitRepository;
            }
        }

        private IRoadRepository roadRepository;
        public IRoadRepository RoadRepository
        {
            get
            {
                if (roadRepository == null)
                {
                    roadRepository = new RoadRepository(this.context);
                }

                return roadRepository;
            }
        }

        private IUserAuditTrailRepository auditRepository;
        public IUserAuditTrailRepository UserAuditTrailRepository
        {
            get
            {
                if (auditRepository == null)
                {
                    auditRepository = new UserAuditTrailRepository(this.context);
                }

                return auditRepository;
            }
        }

        private IGpsCoordinateRepository gpsCoordinateRepository;
        public IGpsCoordinateRepository GpsCoordinateRepository
        {
            get
            {
                if (gpsCoordinateRepository == null)
                {
                    gpsCoordinateRepository = new GpsCoordinateRepository(this.context);
                }

                return gpsCoordinateRepository;
            }
        }

        private IRouteRepository routeRepository;
        public IRouteRepository RouteRepository
        {
            get
            {
                if (routeRepository == null)
                {
                    routeRepository = new RouteRepository(this.context);
                }

                return routeRepository;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }


        public void SaveChanges()
        {
            //TODO: Ver transaction
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }


        #endregion
    }
}
