using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OhlalaWebWithAuth
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Accounts",
            //    url: "Account/{action}",
            //    defaults: new { controller = "Account", action = "Login" }
            //);

            //routes.MapRoute(
            //    name: "Events",
            //    url: "Loader/{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Loader", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Menu",
            //    url: "{action}/{identifier}",
            //    defaults: new { controller = "Home", action = "Index", identifier = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{identifier}",
                defaults: new { controller = "Home", action = "Index", identifier = UrlParameter.Optional }
            );
        }
    }
}
