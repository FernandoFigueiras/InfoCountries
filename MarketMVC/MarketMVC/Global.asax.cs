using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MarketMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()//onde a aplicacao arranca
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Data.MarketMVCContext, Migrations.Configuration>());//inicializar sempre a base de dados e registar as mudancas que haja
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
