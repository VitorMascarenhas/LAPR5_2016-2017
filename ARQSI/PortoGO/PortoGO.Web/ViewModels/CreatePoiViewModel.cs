using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortoGO.Web.ViewModels
{
    public class CreatePoiViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int LocationId { get; set; }

        public List<LocationViewModel> Locations { get; set; }

        [Required]
        [Display(Name ="Business Hours - From"), DataType(DataType.Time)]
        public TimeSpan? BusinessHoursFromHour { get; set; }

        [Required]
        [Display(Name = "Business Hours - To"), DataType(DataType.Time)]
        public TimeSpan? BusinessHoursToHour { get; set; }

        [Required]
        [Display(Name = "Time it takes to visit")]
        public double? TimeTovisit { get; set; }

        public string Hashtags { get; set; }

        public CreatePoiViewModel()
        {
            this.Locations = new List<LocationViewModel>();
        }
    }
}