using System;
using System.Collections.Generic;
using System.Linq;
using SceneCrm.Entities;

namespace SceneAndHeard.Support
{
    public class InitialisesVolunteerAllocationView
    {
        public void Initialise(dynamic viewBag, SceneCRM context)
        {
            Initialise(viewBag, context, new VolunteerAllocation[0]);
        }

        public void Initialise(dynamic viewBag, SceneCRM context, IEnumerable<VolunteerAllocation> currentVolunteers)
        {

            var volunteers = context.Volunteers
                .ToArray()
                .OrderBy(v => v.IsEligible)                
                .Select(v => new { Id = v.VolunteerId.ToString(), Name = string.Format("{0}, {1}{2}", v.Surname, v.FirstName, v.IsEligible ? string.Empty : " (NOT ELIGIBLE)") })
                .ToDictionary(v => v.Id, v => v.Name);

            var jobs = context.Jobs
                .Select(j => new { Id = j.JobId, Name = j.Description })
                .ToDictionary(j => j.Id.ToString(), j => j.Name);

            var currentVolunteerModel = currentVolunteers
                .Select(cv => new { jobId = cv.JobId.ToString(), volunteerId = cv.VolunteerId.ToString(), notes = cv.Notes })
                .OrderBy(cv => cv.jobId);

            viewBag.CurrentVolunteers = currentVolunteerModel;
            viewBag.PossibleVolunteers = volunteers;
            viewBag.PossibleJobs = jobs;
        }
    }
}