using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Configuration
{
    public class RoadEntityConfiguration : EntityTypeConfiguration<Road>
    {
        public RoadEntityConfiguration()
        {
            Property(p => p.Name).HasMaxLength(300);
        }
    }
}
