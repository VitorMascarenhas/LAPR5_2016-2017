using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortoGO.Web.ViewModels
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

        public virtual ICollection<GpsCoordinatesViewModel> Nodes { get; set; }

        public RouteViewModel()
        {
            this.Nodes = new List<GpsCoordinatesViewModel>();
        }
    }
}