using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Domain
{
    public class Visit
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

        /// <summary>
        /// Gets or sets the start location.
        /// </summary>
        /// <value>
        /// The start location.
        /// </value>
        public Location StartLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [return to start].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [return to start]; otherwise, <c>false</c>.
        /// </value>
        public bool ReturnToStart { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets the points of interests.
        /// </summary>
        /// <value>
        /// The points of interests.
        /// </value>
        public virtual ICollection<PointOfInterest> PointsOfInterests { get; private set; }

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
        public virtual ICollection<Route> Route { get; private set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="Visit"/> class from being created.
        /// </summary>
        private Visit() 
        {
            this.PointsOfInterests = new HashSet<PointOfInterest>();
            this.Route = new HashSet<Route>();

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Visit"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="returnToStart">if set to <c>true</c> [return to start].</param>
        public Visit(string name, DateTime startDate, DateTime endDate, bool returnToStart, string userId, int duration) : this()
        {
            this.Name = name;
            this.StartDate = startDate;
            this.Enddate = endDate;
            this.ReturnToStart = returnToStart;
            this.UserId= userId;
            this.Duration = duration;
        }
    }
}
