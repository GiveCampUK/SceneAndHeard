using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SceneAndHeardFeedback.Models
{
    public class FeedbackService
    {
        private FeedbackContext _context;
        public FeedbackService()
        {
            _context = new FeedbackContext();
        }

        public void saveFeedback(Feedback feedback)
        {
            _context.Feedback.Add(feedback);
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