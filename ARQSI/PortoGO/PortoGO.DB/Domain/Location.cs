using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Domain
{
    /// <summary>
    /// Represents a location in a Map
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        public GpsCoordinate Coordinates { get; set; }

        /// <summary>
        /// Gets the points of interest.
        /// </summary>
        /// <value>
        /// The points of interest.
        /// </value>
        public virtual ICollection<PointOfInterest> PointsOfInterest { get; private set; }

        /// <summary>
        /// Gets or sets the location name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="Location"/> class from being created.
        /// </summary>
        private Location()
        {
            if (this.Coordinates == null)
            {
                this.Coordinates = new GpsCoordinate();
            }

            this.PointsOfInterest = new HashSet<PointOfInterest>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="coordinates">The coordinates.</param>
        //public Location(string name, double latitude, double longitude) : this()
        //{
        //    this.Coordinates = new GpsCoordinate();

        //    this.Name = name;
        //    this.Coordinates.Latitude = latitude;
        //    this.Coordinates.Longitude = longitude;
        //}

        public Location(string name, GpsCoordinate coordinates) : this()
        {
            if (coordinates == null)
                throw new ArgumentNullException("coordinates");

            //if (string.IsNullOrEmpty(name))
            //    throw new ArgumentNullException("name");

            this.Coordinates = coordinates;
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude.</param>
        //public Location(string name, double latitude, double longitude, double altitude) : this(name, latitude, longitude)
        //{
        //    if(this.Coordinates == null)
        //    {
        //        this.Coordinates = new GpsCoordinate();
        //    }

        //    this.Coordinates.Altitude = altitude;
        //}
    }
}
