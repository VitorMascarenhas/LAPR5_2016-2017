using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PortoGO.DB.Repositories
{
    public class RoadRepository : BaseRepository<Road, int>, IRoadRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoadRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RoadRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the all the roads between the coordinates.
        /// </summary>
        /// <param name="fromLatitude">From latitude.</param>
        /// <param name="fromLongitude">From longitude.</param>
        /// <param name="toLatitude">To latitude.</param>
        /// <param name="toLongitude">To longitude.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Road> Get(float fromLatitude, float fromLongitude, float toLatitude, float toLongitude)
        {
            DbSet<Road> data = this.Context.Set<Road>();

            IQueryable<Road> result = data.Where(x => x.RoadCoordinates.Any(z => z.Longitude >= fromLongitude)
                                        && x.RoadCoordinates.Any(w => w.Longitude <= toLongitude)
                                        && x.RoadCoordinates.Any(y => y.Latitude >= fromLatitude)
                                        && x.RoadCoordinates.Any(k => k.Latitude <= toLatitude)).Include("RoadCoordinates");

            return result.ToList();
        }
    }
}
