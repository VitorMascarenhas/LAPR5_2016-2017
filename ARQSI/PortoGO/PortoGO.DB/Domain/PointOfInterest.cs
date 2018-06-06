using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Domain
{
    /// <summary>
    /// Represents a point of interest
    /// </summary>
    public class PointOfInterest
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the point of interest description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the business hours.
        /// </summary>
        /// <value>
        /// The business hours.
        /// </value>
        public BusinessHours BusinessHours { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public long LocationId { get; set; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }


        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the time it takes to visit the point of interest.
        /// </summary>
        /// <value>
        /// The time tovisit.
        /// </value>
        public double TimeTovisit { get; set; }

        /// <summary>
        /// Gets or sets the status. (Pending or approved)
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Status Status { get; set; }

        /// <summary>
        /// Gets the hashtags associated with the point of interest.
        /// </summary>
        /// <value>
        /// The hashtags.
        /// </value>
        public virtual ICollection<Hashtag> Hashtags { get; private set; }

        /// <summary>
        /// Gets the visits associated with the point of interest
        /// </summary>
        /// <value>
        /// The visits.
        /// </value>
        public virtual ICollection<Visit> Visits { get; private set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="PointOfInterest"/> class from being created.
        /// </summary>
        private PointOfInterest() //for EF
        {
            this.Hashtags = new HashSet<Hashtag>();
            this.Visits = new HashSet<Visit>();
            this.Status = Status.Pending;

            if (this.BusinessHours == null)
            {
                this.BusinessHours = new BusinessHours();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterest"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public PointOfInterest(long locationId, string description, BusinessHours businessHours, double timeToVosit, string userId) : this()
        {
            this.LocationId = locationId;
            this.Description = description;
            this.BusinessHours = businessHours;
            this.TimeTovisit = timeToVosit;
            this.UserId = userId;
        }

        public PointOfInterest(Location location, string description, BusinessHours businessHours, double timeToVosit, string userId) : this()
        {
            this.Location = location;
            if (location != null)
            {
                this.LocationId = location.Id;
            }

            this.Description = description;
            this.BusinessHours = businessHours;
            this.TimeTovisit = timeToVosit;
            this.UserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterest"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="description">The description.</param>
        /// <param name="businessHours">The business hours.</param>
        /// <param name="timeToVosit">The time to vosit.</param>
        /// <param name="user">The user.</param>
        public PointOfInterest(Location location, string description, BusinessHours businessHours, double timeToVosit, User user) : this(location, description, businessHours, timeToVosit, user.Id)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterest"/> class.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="businessHours">The business hours.</param>
        /// <param name="timeToVosit">The time to vosit.</param>
        /// <param name="user">The user.</param>
        public PointOfInterest(long locationId, string description, BusinessHours businessHours, double timeToVosit, User user) : this(locationId, description, businessHours, timeToVosit, user.Id)
        {

        }

        /// <summary>
        /// Sets the point of interest status to approved.
        /// </summary>
        public void Approve()
        {
            this.Status = Status.Approved;
        }

        public void Reject()
        {
            this.Status = Status.Rejected;
        }

        public bool IsOwner(string userId)
        {
            return this.UserId == userId;
        }

    }
}
