using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PortoGO.DB.Repositories
{
    public class PointOfInterestRepository : BaseRepository<PointOfInterest, int>, IPointOfInterestRepository
    {
        public PointOfInterestRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PointOfInterest> GetUserPointsOfInterest(string userId, bool includeHashtags = false)
        {
            if (!includeHashtags)
            {
                return this.Find(x => x.UserId == userId, null, "Location,User,Location.Coordinates");
            }
            else
            {
                return this.Find(x => x.UserId == userId, null, "Location,User,Hashtags,Location.Coordinates");
            }

        }
    }
}
