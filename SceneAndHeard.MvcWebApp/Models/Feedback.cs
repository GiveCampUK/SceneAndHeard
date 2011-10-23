using System;
using System.ComponentModel.DataAnnotations;

namespace SceneAndHeardFeedback.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string FeedbackText { get; set; }
        public DateTime FeedbackLeft { get; set; }
        [Required]
        public string eventBriteId { get; set; }
        [Required]
        public string ContactName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ContactEmailAddress { get; set; }
        public string OrganisationName { get; set; }
        public bool Approved { get; set; }
    }
}