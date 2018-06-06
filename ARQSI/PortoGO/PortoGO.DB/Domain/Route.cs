using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Domain
{
    public class Route
    {
        public int Id { get; set; }

        public int VisitId { get; set; }

        [ForeignKey("VisitId")]
        public virtual Visit Visit { get; set; }

        public int Order { get; set; }

        public TimeSpan Hour { get; set; }

        public TimeSpan RunTime { get; set; }

        public int PointOfInterestId { get; set; }

        [ForeignKey("PointOfInterestId")]
        public virtual PointOfInterest PointOfInterest { get; set; }

        public virtual ICollection<GpsCoordinate> Nodes { get; private set; }

        public Route()
        {
            this.Nodes = new List<GpsCoordinate>();
        }
    }
}
