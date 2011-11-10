using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SceneCrm.Entities;

namespace SceneAndHeardFeedback.Models
{
    public class FeedbackService
    {
        private SceneCRM _context = new SceneCRM();
        public FeedbackService()
        {

        }

        public void saveFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            _context.Dispose();
        }

        public List<Feedback> getFeedbacks()
        {
            return new List<Feedback>();
        }

        public List<Event> getAllPerformances()
        {
            throw new NotImplementedException();
        }
    }
}