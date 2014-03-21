using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSite.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Blog", action = "AllBlogs", id = UrlParameter.Optional }
            //);

            routes.MapRoute(null,
            "", // Only matches the empty URL (i.e. /)
            new
            {
                controller = "Home",
                action = "Index",
                page = 1
            }
            );

            routes.MapRoute(null,
                "Page{page}", // Matches /Page2, /Page123, but not /PageXYZ
                new { controller = "Blog", action = "AllBlogs" },
                new { page = @"\d+" } // Constraints: page must be numerical
                );

            routes.MapRoute(null,
                "{category}", // Matches /Football or /AnythingWithNoSlash
                new { controller = "Blog", action = "AllBlogs", page = 1 }
                );

            //routes.MapRoute(null,
            //    "{category}/Page{page}", // Matches /Football/Page567
            //    new { controller = "Blog", action = "AllBlogs" }, // Defaults
            //    new { page = @"\d+" } // Constraints: page must be numerical
            //    );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}