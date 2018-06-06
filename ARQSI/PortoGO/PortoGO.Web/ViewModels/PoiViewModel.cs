using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortoGO.DB.Domain;
using System.ComponentModel.DataAnnotations;

namespace PortoGO.Web.ViewModels
{
    public class PoiViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Business Hours - From"), DataType(DataType.Time)]
        public TimeSpan? BusinessHoursFromHour { get; set; }

        [Required]
        [Display(Name = "Business Hours - To"), DataType(DataType.Time)]
        public TimeSpan? BusinessHoursToHour { get; set; }

        [Required]
        [Display(Name ="Location")]
        public int LocationId { get; set; }

        public LocationViewModel Location { get; set; }

        [Required]
        [Display(Name ="Time it takes to visit (h)")]
        public double? TimeTovisit { get; set; }

        public Status Status { get; set; }

        public string HashtagString { get; set; }

        [Display(Name = "Hashtags")]
        public virtual ICollection<HashtagViewModel> Hashtags { get; set; }

        public bool IsOwner { get; set; }
    }
}
