using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SceneAndHeardFeedback.Models
{
    public class FeedbackContext : DbContext
    {
        public DbSet<Feedback> Feedback { get; set; }
    }
}