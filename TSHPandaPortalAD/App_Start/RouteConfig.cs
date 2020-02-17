using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TSHPandaPortalAD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Panel", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Niky"
                , "{controller}/{action}");

            routes.MapRoute(
                name: "SetType",
                url: "دسته-بندی-محصولات/{HasChang}/{Id}",
                defaults: new { controller = "Panel", action = "SetType", id = UrlParameter.Optional }
            );
        }
    }
}
