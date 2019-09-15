using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Foodspotting.Startup))]
namespace Foodspotting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
