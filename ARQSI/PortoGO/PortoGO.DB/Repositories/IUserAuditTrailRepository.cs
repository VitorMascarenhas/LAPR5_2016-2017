using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Repositories
{
    public interface IUserAuditTrailRepository : IBaseRepository<UserAuditTrail, int>
    {
        void LogAccountCreation(User user);

        void LogAccountDeletion(User user);
    }
}
