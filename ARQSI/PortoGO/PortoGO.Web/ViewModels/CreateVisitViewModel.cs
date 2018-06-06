using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortoGO.Web.ViewModels
{
    public class CreateVisitViewModel
    {
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
        public string StartLocation { get; set; }

        [Display(Name = "Do you want to return to start location?")]
        public bool ReturnToStart { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}