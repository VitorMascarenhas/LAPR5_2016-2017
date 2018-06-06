using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortoGo.WebApi.Models
{
    public class PoiViewModel
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
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the business hours.
        /// </summary>
        /// <value>
        /// The business hours.
        /// </value>
        [Required]
        public BusinessHours BusinessHours { get; set; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public LocationViewModel Location { get; set; }

        /// <summary>
        /// Gets or sets the time it takes to visit the point of interest.
        /// </summary>
        /// <value>
        /// The time tovisit.
        /// </value>
        [Required]
        public double TimeTovisit { get; set; }

        /// <summary>
        /// Gets or sets the status. (Pending or approved)
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Status Status { get; set; }

        public virtual ICollection<HashtagViewModel> Hashtags { get; set; }
    }
}