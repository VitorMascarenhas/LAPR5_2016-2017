﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortoGO.Web.ViewModels
{
    public class GpsCoordinatesViewModel
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? Altitude { get; set; }
    }
}