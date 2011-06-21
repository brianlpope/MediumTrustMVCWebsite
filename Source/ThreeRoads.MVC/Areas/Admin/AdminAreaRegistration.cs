using System.Web.Mvc;

namespace ThreeRoads.MVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminArticle",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Article", action = "Index", id = UrlParameter.Optional },
                new { controller="(Article|Bio|Event|Download|Resource|QA)", action="(Index|Detail|Delete|Edit|Create)" }
            );

        }
    }
}
