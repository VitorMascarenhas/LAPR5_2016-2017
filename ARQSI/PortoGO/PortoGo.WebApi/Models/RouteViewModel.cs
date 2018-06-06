using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortoGo.WebApi.Models
{
    public class RouteViewModel
    {
        public long Id { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public TimeSpan Hour { get; set; }

        [Required]
        public TimeSpan RunTime { get; set; }

        public PoiViewModel PointOfInterest { get; set; }

        public int PoiId { get; set; }

        [Required]
        public int VisitId { get; set; }

        [Required]
        public virtual ICollection<GpsCoordinatesViewModel> Nodes { get; private set; }

        public RouteViewModel()
        {
            this.Nodes = new List<GpsCoordinatesViewModel>();
        }
    }
}