using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Configuration
{
    public class RouteEntityTypeConfiguration : EntityTypeConfiguration<Route>
    {
        public RouteEntityTypeConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(p => p.VisitId).IsRequired();
        }
    }
}
