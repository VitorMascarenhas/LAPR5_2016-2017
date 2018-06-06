using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PortoGO.DB.Repositories
{
    public class GpsCoordinateRepository : BaseRepository<GpsCoordinate, long>, IGpsCoordinateRepository
    {
        public GpsCoordinateRepository(DbContext context) : base(context)
        {
        }
    }
}
