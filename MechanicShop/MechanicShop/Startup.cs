using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MechanicShop.Startup))]
namespace MechanicShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
