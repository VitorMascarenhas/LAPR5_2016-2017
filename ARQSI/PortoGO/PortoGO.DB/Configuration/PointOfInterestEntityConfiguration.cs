using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Configuration
{
    public class PointOfInterestEntityConfiguration : EntityTypeConfiguration<PointOfInterest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterestEntityConfiguration"/> class.
        /// </summary>
        public PointOfInterestEntityConfiguration()
        {
            Property(p => p.Description).HasMaxLength(300);
        }
    }
}
