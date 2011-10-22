using System;
using System.Collections.Generic;
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
        public bool HasACrbCheck
        {
            get { return CrbChecks.Count > 0; }
        }

        public bool HasValidCrbCheck
        {
            get {
                //return CrbChecks.Max(crb => crb.DateCheckExpires > DateTime.Today); }
                return (false);
            }
        }

        public bool HasAttendedAPerformance
        {
            get { return !String.IsNullOrEmpty(PerformanceAttended); }
        }

        public bool IsEligible
        {
            get { return HasACrbCheck && HasValidCrbCheck && HasAttendedAPerformance; }
        }
    }
}
