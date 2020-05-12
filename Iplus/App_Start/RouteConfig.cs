using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Iplus
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AngularClient",
                url: "ng/{*angularRoute}",
                defaults: new { controller = "Home", action = "AdminPanel" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index",
                    id = UrlParameter.Optional},
                namespaces: new string[]{ "Iplus.Controllers" });

            //routes.MapRoute(
            //    name: "Api",
            //    url: "api/{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "EverythingElse",
                url: "{*everythingElse}",
                defaults: new { controller = "Home", action = "AdminPanel" });

        }
    }
}
