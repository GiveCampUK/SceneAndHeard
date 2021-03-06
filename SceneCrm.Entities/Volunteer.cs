//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SceneCrm.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Volunteer
    {
        public Volunteer()
        {
            this.CourseVolunteers = new HashSet<CourseVolunteer>();
            this.CrbChecks = new HashSet<CrbCheck>();
            this.PlayVolunteers = new HashSet<PlayVolunteer>();
            this.ProductionVolunteers = new HashSet<ProductionVolunteer>();
            this.Jobs = new HashSet<Job>();
            this.Referrals = new HashSet<Volunteer>();
        }
    
        public int VolunteerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PerformanceAttended { get; set; }
        public Nullable<System.DateTime> AvailableFrom { get; set; }
        public Nullable<int> ReferredByVolunteerId { get; set; }
        public string PartnerFirstName { get; set; }
        public string PartnerSurname { get; set; }
        public string Notes { get; set; }
        public Nullable<int> AccessPersonId { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string EmailAddress { get; set; }
        public string EmailAddress2 { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string CvWebUrl { get; set; }
        public Nullable<bool> SpotlightNumber { get; set; }
        public string AgentName { get; set; }
        public string Organisation { get; set; }
        public Nullable<bool> EyesEars { get; set; }
        public Nullable<bool> Schools { get; set; }
        public Nullable<bool> Trustee { get; set; }
        public Nullable<bool> Deadwood { get; set; }
        public Nullable<bool> NoMailout { get; set; }
        public Nullable<bool> EEDirectDebit { get; set; }
        public string OtherProfession { get; set; }
    
        public virtual ICollection<CourseVolunteer> CourseVolunteers { get; set; }
        public virtual ICollection<CrbCheck> CrbChecks { get; set; }
        public virtual ICollection<PlayVolunteer> PlayVolunteers { get; set; }
        public virtual ICollection<ProductionVolunteer> ProductionVolunteers { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Volunteer> Referrals { get; set; }
        public virtual Volunteer ReferredBy { get; set; }
    }
}
