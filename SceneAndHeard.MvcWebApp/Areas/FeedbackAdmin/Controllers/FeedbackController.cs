using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SceneAndHeardFeedback.Models;
using Util.ConfigManager;
using SceneCrm.Entities;

namespace SceneAndHeard.Areas.FeedbackAdmin.Controllers
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

            return View(events);
        }

        public ActionResult ReviewFeedback(Int64 id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReviewFeedback(List<Feedback> feedbacks)
        {
            foreach (Feedback feedback in feedbacks)
            {
                // Save each feedback through the Feedback service
                _service.saveFeedback(feedback);
            }

            return RedirectToAction("Index");
        }
    }
}
