using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortoGO.Web.Startup))]
namespace PortoGO.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
