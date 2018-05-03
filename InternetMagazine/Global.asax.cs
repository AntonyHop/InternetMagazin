using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using InternetMagazine.PL.Infrastructure;
using InternetMagazine.Util;

namespace InternetMagazine
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            NinjectModule categoryModule = new CategoryModule();
            NinjectModule serviceModule = new ServiceModule(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InternetMagazine;Integrated Security=True");
            var kernel = new StandardKernel(serviceModule, categoryModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
