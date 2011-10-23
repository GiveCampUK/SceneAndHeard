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
            get { return CrbChecks.Count > 0 && CrbChecks.Any(crb => crb.ApprovalDate.HasValue && crb.ApprovalDate.Value.AddYears(3) > DateTime.Today); }
        }

        public bool HasAttendedAPerformance
        {
            get { return !String.IsNullOrEmpty(PerformanceAttended); }
        }

        public bool HasReferrer { get { return _ReferredByVolunteerId.HasValue; } }

        public string CrbExpiry
        {
            get
            {
                if (CrbChecks == null || CrbChecks.Count == 0) return "None";

                var maxDate = CrbChecks.Max(c => c.ApprovalDate.HasValue ? c.ApprovalDate.Value.AddYears(3) : (DateTime?)null);

                if (maxDate.HasValue)
                {
                    return maxDate.Value.ToShortDateString();
                }

                return "None";
            }
        }

    }
}