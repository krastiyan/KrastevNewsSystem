using System.Web.Mvc;

namespace KrastevNewsSystem.Areas.UsersFeatures
{
    public class UsersFeaturesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UsersFeatures";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UsersFeatures_default",
                "UsersFeatures/{controller}/{action}/{id}"
                , new { action = "Index", id = UrlParameter.Optional }
                //,
                //namespaces: new[] { "KrastevNewsSystem.Areas.UsersFeatures.Controllers" }
            );
        }
    }
}