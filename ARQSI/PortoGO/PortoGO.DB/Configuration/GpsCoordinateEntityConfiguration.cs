using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Configuration
{
    public class GpsCoordinateEntityConfiguration : EntityTypeConfiguration<GpsCoordinate>
    {
        public GpsCoordinateEntityConfiguration()
        {
            this.HasKey(k => k.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
