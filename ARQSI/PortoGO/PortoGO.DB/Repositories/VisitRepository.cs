using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public class VisitRepository : BaseRepository<Visit, int>, IVisitRepository
    {
        public VisitRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Visit> Get(string userId, string includeProperties = "")
        {
            return this.Find(x => x.User.Id == userId, null, includeProperties);
        }
    }
}
