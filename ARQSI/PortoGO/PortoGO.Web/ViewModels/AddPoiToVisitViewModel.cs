using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortoGO.Web.ViewModels
{
    public class AddPoiToVisitViewModel
    {
        public int Id { get; set; }

        public int LocationId { get; set; }

        public int MyProperty { get; set; }

        public LocationViewModel Location { get; set; }
    }
}