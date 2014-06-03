using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit
{
    public class RouteConfig
    {
        private static RouteCollection _routes;
        public static void RegisterRoutes(RouteCollection routes)
        {
            _routes = routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (PageTypes pageType in Enum.GetValues(typeof(PageTypes)))
            {
                AddRoute(Routes.NavigationItems[pageType]);
            }

//            routes.MapRoute(
//name: "Discussion_ID",
//url: "Discussion/{id}",
//defaults: new { controller = "Discussion", action = "Index", id = UrlParameter.Optional }
//);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

  

            //var prof = routes.MapRoute(
            //            name: "MyProfile",
            //            url: "Profile/{id}",
            //            defaults: new { controller = "FullProfile", action = "Index", id = UrlParameter.Optional }
            //        );
            //prof.DataTokens["area"] = "Profile";

            //var useprof = routes.MapRoute(
            //                name: "UserProfile",
            //                url: "{id}",
            //            defaults: new { controller = "FullProfile", action = "Index"}
            //        );
            //useprof.DataTokens["area"] = "Profile";
        }

        private static void AddRoute(NavigationItem nav)
        {
            var alumniRoute = _routes.MapRoute(
                name: nav.Controller + nav.Action,
                url: nav.RoutingText + "/{id}",
                defaults: new { controller = nav.Controller, action = nav.Action, id = UrlParameter.Optional }
            );
            alumniRoute.DataTokens["area"] = nav.Area;
        }
    }
}