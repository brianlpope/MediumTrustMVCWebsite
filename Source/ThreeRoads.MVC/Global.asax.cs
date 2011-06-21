using System.Web.Mvc;
using System.Web.Routing;

namespace ThreeRoads.MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "IntroArticles",
                "introduction/{topic}",
                new { controller = "Composite", action = "Article", topic = "values" },
                new { topic = "(values|classical|christian|mission|vision|resources|coalition)" }
            );
            routes.MapRoute(
                "IntroActions",
                "introduction/{action}",
                new { controller = "Composite" },
                new { action = "(questions|welcome)" }
            );

            routes.MapRoute(
                "AdmissionArticles",
                "admissions/{topic}",
                new { controller = "Composite", action = "Article", topic = "tuition" },
                new { topic = "(tuition|nextsteps|assistance)" }
            );
            routes.MapRoute(
                "AdmissionActions",
                "admissions/downloads",
                new { controller = "Composite", action = "downloads" }
            );
            routes.MapRoute(
                "InsideArticles",
                "inside/{topic}",
                new { controller = "Composite", action = "Article", topic = "admission" },
                new { topic = "(admission|curriculum|uniforms|scholarships)" }
            );
            routes.MapRoute(
                "InsideActions",
                "inside/{action}",
                new { controller = "Composite" },
                new { action = "(staff|board|calendar|erb)" }
            );

            /* Account routes */
            routes.MapRoute(
                "Account",
                "account/{action}",
                new { controller = "Account", action = "LogOn" },
                new { action = "(logon|logoff|register|changepassword|changepasswordsuccess)" }
            );

            /* Contact routes */
            routes.MapRoute(
                "ContactActions",
                "contact/{action}",
                new { controller = "Composite", action = "Contact" },
                new { action = "(contact|location)" }
            );

            routes.MapRoute(
                "LegacyArticles",
                "legacy/{topic}",
                new { controller = "Composite", action = "Article", topic = "fundraising" },
                new { topic = "(missionproject|fundraising|wishlist|tellafriend)" }
            );

            routes.MapRoute(
                "LegacyActions",
                "legacy/{action}",
                new { controller = "Composite", action = "donate" },
                new { action = "(donate)" }
            );

            routes.MapRoute(
                "SearchResults",
                "search/{action}/{query}",
                new { controller = "Composite", action = "SearchResults", query = "" },
                new { action = "(SearchResults)" }
            );

            routes.MapRoute(
                "GalleryActions",
                "inside/{action}",
                new { controller = "Gallery" },
                new { action = "(gallery|galleries)" }
            );

            // catch everything else
            routes.MapRoute("catchall",
                "{*catchall}",
                new { controller = "Composite", action = "Welcome" });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);


        }
    }
}