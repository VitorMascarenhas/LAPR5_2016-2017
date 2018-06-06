using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public interface IGpsCoordinateRepository : IBaseRepository<GpsCoordinate, long>
    {
    }
}
