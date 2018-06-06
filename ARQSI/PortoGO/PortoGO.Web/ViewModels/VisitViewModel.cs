using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortoGO.Web.ViewModels
{
    public class VisitViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start Date"), DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date"), DataType(DataType.Date)]
        public DateTime Enddate { get; set; }

        [Required]
        [Display(Name = "Start Location")]
        public LocationViewModel StartLocation { get; set; }

        [Display(Name ="Do you want to return to start location?")]
        public bool ReturnToStart { get; set; }

        [Required]
        public virtual ICollection<PoiViewModel> PointsOfInterests { get; set; }

        [Display(Name = "Point of Interest")]
        public virtual ICollection<PoiViewModel> PoisToAdd { get; set; }

        [Display(Name ="Point of Interest")]
        public int PoiToAddId { get; set; }

        public virtual ICollection<RouteViewModel> Routes { get; set; }
    }
}










