using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Domain
{
    public class GpsCoordinate
    {
        public long Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? Altitude { get; set; }

        public virtual ICollection<Road> Roads { get; set; }

        public virtual ICollection<Route> Routes { get; private set; }

        public GpsCoordinate()
        {
            this.Roads = new HashSet<Road>();

            this.Routes = new HashSet<Route>();
        }
    }
}
