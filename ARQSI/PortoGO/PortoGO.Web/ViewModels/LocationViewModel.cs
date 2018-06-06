using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortoGO.Web.ViewModels
{
    public class LocationViewModel
    {
        public int Id { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float? Altitude { get; set; }

        public string Name { get; set; }
    }
}