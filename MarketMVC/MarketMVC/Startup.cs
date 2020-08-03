using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketMVC.Startup))]
namespace MarketMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
