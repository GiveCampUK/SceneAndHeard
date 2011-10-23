using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace SceneAndHeard.Support
{
    public class InterpretsPostedVolunteerAllocations
    {


        public IEnumerable<VolunteerAllocation> Interpret(NameValueCollection form)
        {
            var keyNumbers =
                form.AllKeys
                    .Where(key => key.StartsWith("volunteer", StringComparison.InvariantCultureIgnoreCase))
                    .Select(key => key.Substring(9));

            foreach (var keyNumber in keyNumbers)
            {
                var volunteerIdString = form[string.Format("volunteer{0}", keyNumber)];
                var jobIdString = form[string.Format("job{0}", keyNumber)];
                var notes = form[string.Format("notes{0}", keyNumber)];

                if (volunteerIdString != null && jobIdString != null)
                {
                    int volunteerId;
                    int jobId;

                    if (int.TryParse(volunteerIdString, out volunteerId) && int.TryParse(jobIdString, out jobId))
                        yield return new VolunteerAllocation(volunteerId, jobId, notes);
                }

            }

        }
    }
}