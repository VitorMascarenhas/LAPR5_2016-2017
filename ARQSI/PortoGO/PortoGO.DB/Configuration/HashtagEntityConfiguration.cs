using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Configuration
{
    public class HashtagEntityConfiguration : EntityTypeConfiguration<Hashtag>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HashtagEntityConfiguration"/> class.
        /// </summary>
        public HashtagEntityConfiguration()
        {
            Property(p => p.Tag).HasMaxLength(100);
        }
    }
}
