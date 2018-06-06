using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public interface IRoadRepository : IBaseRepository<Road,int>
    {
        /// <summary>
        /// Gets the all the roads between the coordinates.
        /// </summary>
        /// <param name="fromLatitude">From latitude.</param>
        /// <param name="fromLongitude">From longitude.</param>
        /// <param name="toLatitude">To latitude.</param>
        /// <param name="toLongitude">To longitude.</param>
        /// <returns></returns>
        IEnumerable<Road> Get(float fromLatitude, float fromLongitude, float toLatitude, float toLongitude);
        
    }
}
