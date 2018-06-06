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
    public class LocationEntityConfiguration : EntityTypeConfiguration<Location>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationEntityConfiguration"/> class.
        /// </summary>
        public LocationEntityConfiguration()
        {
            HasKey(k => k.Id);
            Property(p => p.Name).HasMaxLength(100);
        }
    }
}
