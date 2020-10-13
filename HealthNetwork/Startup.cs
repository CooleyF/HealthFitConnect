using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthNetwork.Startup))]
namespace HealthNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
