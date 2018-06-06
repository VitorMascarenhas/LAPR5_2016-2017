using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PortoGO.DB.Repositories
{
    public class UserAuditTrailRepository : BaseRepository<UserAuditTrail, int>, IUserAuditTrailRepository
    {
        public UserAuditTrailRepository(DbContext context) : base(context)
        {
        }

        public void LogAccountCreation(User user)
        {
            this.Insert(Log(user, UserAuditTrailType.Add));
        }

        public void LogAccountDeletion(User user)
        {
            this.Insert(Log(user, UserAuditTrailType.Remove));
        }

        private UserAuditTrail Log(User user, UserAuditTrailType type)
        {
            var log = new UserAuditTrail
            {
                TimeStamp = DateTime.Now,
                Type = type,
                UserName = user.UserName
            };

            return log;
        }
    }
}
