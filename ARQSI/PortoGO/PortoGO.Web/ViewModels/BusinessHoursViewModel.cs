using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortoGO.Web.ViewModels
{
    public class BusinessHoursViewModel
    {

        public TimeSpan FromHour { get; set; }

        public TimeSpan ToHour { get; set; }
    }
}