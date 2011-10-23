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
        public List<Tuple<Volunteer, Job>> AllAssociatedVolunteers
        {
            get
            {
                var v = new List<Tuple<Volunteer, Job>>();
                foreach (ProductionVolunteer volunteer in ProductionVolunteers)
                    v.Add(new Tuple<Volunteer, Job>(volunteer.Volunteer, volunteer.Job));

                foreach (var playvol in Plays.SelectMany(play => play.PlayVolunteers))
                {

                    if (!v.Contains(new Tuple<Volunteer, Job>(playvol.Volunteer, playvol.Job)))
                    {
                        v.Add(new Tuple<Volunteer, Job>(playvol.Volunteer, playvol.Job));
                    }
                }
                return v.AsQueryable().OrderBy(x => x.Item2.Description).ThenBy(x => x.Item1.Name).ToList();
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
