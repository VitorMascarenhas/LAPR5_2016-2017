using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Domain
{
    public class UserAuditTrail
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public string  UserName { get; set; }

        public UserAuditTrailType Type { get; set; }
    }
}
