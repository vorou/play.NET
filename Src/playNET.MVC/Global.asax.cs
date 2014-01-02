using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace playNET.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RegisterRoutes();
        }

        private static void RegisterRoutes()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.MapRoute(name: "Default",
                                       url: "{controller}/{action}/{id}",
                                       defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional});
        }
    }
}