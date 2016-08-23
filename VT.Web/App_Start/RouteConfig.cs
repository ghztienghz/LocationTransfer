using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VT.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Location
            routes.MapRoute(
                name: "Location_Home",
                url: "danh-muc/{FriendlyUrl}-{id}.html",
                defaults: new { controller = "Location", action = "Index", FriendlyUrl = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Location_Root",
                url: "{FriendlyUrl}-{id}.html",
                defaults: new { controller = "Location", action = "Root", FriendlyUrl = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Location_Detail",
                url: "chi-tiet/{FriendlyUrl}-{id}.html",
                defaults: new { controller = "Location", action = "Details", FriendlyUrl = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            #endregion



            routes.MapRoute(
                name: "Default_Home",
                url: "",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "Default_action",
                url: "{controller}/{action}.html",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default_parameter",
                url: "{controller}/{action}/{id}.html",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
