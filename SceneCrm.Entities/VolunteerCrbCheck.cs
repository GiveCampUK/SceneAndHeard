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
    
    public partial class VolunteerCrbCheck
    {
        public int VolunteerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PerformanceAttended { get; set; }
        public string CrbNumber { get; set; }
        public Nullable<System.DateTime> ApplicationDate { get; set; }
        public Nullable<System.DateTime> ApplicationExpiryDate { get; set; }
        public Nullable<bool> Approved { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
    }
}
