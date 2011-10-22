using System;
using System.Web.Mvc;

namespace SceneAndHeard.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly EventBriteLayer _eventBriteApi;
        private readonly IConfigManager _configManager;
        private readonly FeedbackService _service;

        public FeedbackController()
        {
            _eventBriteApi = new EventBriteLayer();
            _configManager = new ConfigManager();

            _service = new FeedbackService();
        }

        public ActionResult Index()
        {
            var events = _eventBriteApi.GetEvents(_configManager.GetAppSetting("EventBriteAPIKey"),
                                                  _configManager.GetAppSetting("EventBriteUserKey"),
                                                  _configManager.GetAppSettingAs<int>("EventBriteOrganiserId"));

            var filteredEvents = (from e in events
                                 orderby e.start_date descending
                                 select e).ToList().Take(20).ToList<Event>();

            return View(filteredEvents);
        }

        public ActionResult LeaveFeedback(Int64? id)
        {
            var feedback = new Feedback();
            if (id.HasValue)
            {
                feedback.eventBriteId = id.Value.ToString();
                feedback.FeedbackLeft = DateTime.Today;
                feedback.Approved = false;
            }

            return View(feedback);
        }

        [HttpPost]
        public ActionResult LeaveFeedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _service.saveFeedback(feedback);
                return RedirectToAction("Thanks", "Feedback");
            }
            else
            {
                return View(feedback);
            }
        }

        public ActionResult Thanks()
        {
            return View();
        }

        public JsonResult List()
        {
            // Get the top 10/20 feedbacks, format them as JSON and return them to the caller
            return new JsonResult();
        }
    }
}
