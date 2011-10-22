using System.Web.Mvc;

namespace SceneAndHeard.Areas.FeedbackAdmin
{
    public class FeedbackAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FeedbackAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FeedbackAdmin_default",
                "FeedbackAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
