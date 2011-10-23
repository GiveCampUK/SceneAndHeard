using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneCrm.Entities
{
    public partial class Production
    {
        /// <summary>
        /// All volunteers involved in a production - inc each performance
        /// </summary>
        public Dictionary<Volunteer, Job> AllAssociatedVolunteers
        {
            get
            {
                var v = ProductionVolunteers.ToDictionary(productionVolunteer => productionVolunteer.Volunteer, productionVolunteer => productionVolunteer.Job);

                foreach (var playvol in Plays.SelectMany(play => play.PlayVolunteers))
                {
                    v.Add(playvol.Volunteer, playvol.Job);
                }
                return v;
            }
        }


        public List<Student> AssociatedStudents
        {
            get
            {
                return Plays.Select(play => play.Student).ToList();
            }
        }
    }
}
