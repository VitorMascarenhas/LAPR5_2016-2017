using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortoGo.WebApi.Models
{
    public class StatsViewModel
    {
        public int CountOfRejectedPoi { get; set; }

        public int CountOfApprovedPoi { get; set; }

        public int CountOfPendingPoi { get; set; }

        public int CountOfPoi { get; set; }

        public int CountOfUsers { get; set; }
    }
}