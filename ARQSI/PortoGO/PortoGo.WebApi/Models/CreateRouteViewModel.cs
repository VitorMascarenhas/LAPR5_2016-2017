using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortoGo.WebApi.Models
{
    public class CreateRouteViewModel
    {
        public long Id { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public TimeSpan Hour { get; set; }

        [Required]
        public TimeSpan RunTime { get; set; }

        [Required]
        public int PoiId { get; set; }

        [Required]
        public int VisitId { get; set; }
    }
}