using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Configuration
{
    public class UserAuditTrailEntityConfiguration : EntityTypeConfiguration<UserAuditTrail>
    {
        public UserAuditTrailEntityConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(p => p.UserName).HasMaxLength(256);
        }
    }
}
