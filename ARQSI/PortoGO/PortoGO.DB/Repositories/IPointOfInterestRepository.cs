using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public interface IPointOfInterestRepository : IBaseRepository<PointOfInterest,int>
    {
        IEnumerable<PointOfInterest> GetUserPointsOfInterest(string userId, bool includeHashtags = false);
    }
}
