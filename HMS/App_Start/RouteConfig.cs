using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "FEAccomodations",
                url: "Accomodations",
                defaults: new { area = "", controller = "Accomodations", action = "Index" },
                namespaces: new[] { "HMS.Controllers" }
            );

            routes.MapRoute(
                name: "AccomodationPackageDetails",
                url: "accomodation-package/{accomodationPackageID}",
                defaults: new { area = "", controller = "Accomodations", action = "Details" },
                namespaces: new[] { "HMS.Controllers" }
            );
            
            routes.MapRoute(
                name: "CheckAvailability",
                url: "accomodation-check-availability",
                defaults: new { area = "", controller = "Accomodations", action = "CheckAvailability" },
                namespaces: new[] { "HMS.Controllers" }
            );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
