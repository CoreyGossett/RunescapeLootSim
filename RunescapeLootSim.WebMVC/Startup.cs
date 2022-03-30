using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RunescapeLootSim.WebMVC.Startup))]
namespace RunescapeLootSim.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
