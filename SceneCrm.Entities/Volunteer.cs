using System;
using System.Linq;

namespace SceneCrm.Entities
{
    public partial class Volunteer
    {
        public String Name
        {
            get { return this.FirstName + " " + this.Surname; }

        }

        //1. CRB Check must be active (not expired)
        //2. They must have seen at least one production
        //3. They must have been referred by an existing volunteer

        public bool IsEligible
        {
            get { return HasValidCrbCheck && HasAttendedAPerformance && HasReferrer; }
        }

        public bool HasValidCrbCheck
        {
            //get { return CrbChecks.Count > 0 && CrbChecks.Max(crb => crb.DateCheckExpires > DateTime.Today); }
            get { return true; }
        }

        public bool HasAttendedAPerformance
        {
            get { return !String.IsNullOrEmpty(PerformanceAttended); }
        }

        public bool HasReferrer { get { return _ReferredByVolunteerId.HasValue; } }
    }
}