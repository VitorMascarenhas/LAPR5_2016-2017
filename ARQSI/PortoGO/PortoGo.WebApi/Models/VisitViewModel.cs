using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortoGo.WebApi.Models
{
    public class VisitViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the enddate.
        /// </summary>
        /// <value>
        /// The enddate.
        /// </value>
        public DateTime Enddate { get; set; }

        public LocationViewModel StartLocation { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public UserInfoViewModel User { get; set; }

        /// <summary>
        /// Gets the points of interests.
        /// </summary>
        /// <value>
        /// The points of interests.
        /// </value>
        public virtual ICollection<PoiViewModel> PointsOfInterests { get; set; }

        /// <summary>
        /// Gets or sets the duration of the visit.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>
        /// The route.
        /// </value>
        public virtual ICollection<RouteViewModel> Route { get; set; }

    }
}