using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartFleetManagementSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "DashBoard",
                url: "DashBoard",
                defaults: new { controller = "Home", action = "DashBoard", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Logout",
              url: "Logout",
              defaults: new { controller = "Home", action = "Logout", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Vehicles",
                url: "Vehicles",
                defaults: new { controller = "Car", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "FuelSystems",
            url: "FuelSystems",
            defaults: new { controller = "Fuel", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
               name: "Users",
               url: "Users",
               defaults: new { controller = "Users", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Drivers",
               url: "Drivers",
               defaults: new { controller = "Driver", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
            name: "Concerns",
            url: "Concerns",
            defaults: new { controller = "Concern", action = "Index", id = UrlParameter.Optional }
        );

            routes.MapRoute(
                name: "Reports",
                url: "Reports",
                defaults: new { controller = "Report", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
