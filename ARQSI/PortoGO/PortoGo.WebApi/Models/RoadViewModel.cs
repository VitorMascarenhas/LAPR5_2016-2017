using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortoGo.WebApi.Models
{
    public class RoadViewModel
    {
        [Required]
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

        public ICollection<GpsCoordinatesViewModel> RoadCoordinates { get; set; }

        public RoadViewModel()
        {
            this.RoadCoordinates = new HashSet<GpsCoordinatesViewModel>();
        }
    }
}