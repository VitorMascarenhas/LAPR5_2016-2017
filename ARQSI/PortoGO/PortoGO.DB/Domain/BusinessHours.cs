using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Domain
{
    /// <summary>
    /// Represents the period in which point of interest is available for visiting
    /// </summary>
    public class BusinessHours
    {
        /// <summary>
        /// Gets or sets from hour.
        /// </summary>
        /// <value>
        /// From hour.
        /// </value>
        public TimeSpan FromHour { get; set; }

        /// <summary>
        /// Gets or sets to hour.
        /// </summary>
        /// <value>
        /// To hour.
        /// </value>
        public TimeSpan ToHour { get; set; }
    }
}
