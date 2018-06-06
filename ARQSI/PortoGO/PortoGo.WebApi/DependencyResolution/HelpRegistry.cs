using PortoGo.WebApi.Areas.HelpPage.Controllers;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortoGo.WebApi.DependencyResolution
{
    public class HelpRegistry : Registry
    {
        public HelpRegistry()
        {
            For<HelpController>().Use(ctx => new HelpController());
        }
    }
}