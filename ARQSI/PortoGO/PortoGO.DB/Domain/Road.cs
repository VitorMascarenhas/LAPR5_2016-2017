using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace PortoGO.DB.Domain
{
    /// <summary>
    /// Represents a path between two locations
    /// </summary>
    public class Road
    {
        //private readonly long fromCoordinateId;
        //private readonly long toCoordinateId;

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the weight (for aiding in the calculation of the best path).
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the road width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public float? Width { get; set; }

        /// <summary>
        /// Gets the distance between FromLocation and LoTocation in meters.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        //public double Distance
        //{
        //    get
        //    {
        //        if (FromCoordinates != null && ToCoordinates != null)
        //        {
        //            var sCoord = new GeoCoordinate(FromCoordinates.Latitude, FromCoordinates.Longitude);
        //            var eCoord = new GeoCoordinate(ToCoordinates.Longitude, ToCoordinates.Longitude);

        //            return sCoord.GetDistanceTo(eCoord);
        //        }

        //        return 0;
        //    }
        //}

        public virtual ICollection<GpsCoordinate> RoadCoordinates { get; private set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="Road"/> class from being created.
        /// </summary>
        private Road()
        {
            this.RoadCoordinates = new HashSet<GpsCoordinate>();
        }


        public Road(string name, double weight, float? width) : this()
        {
            this.Name = name;
            this.Weight = weight;
            this.Width = width;
        }
    }
}
