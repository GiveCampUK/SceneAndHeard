namespace SceneAndHeard.Support
{
    public class VolunteerAllocation
    {
        public VolunteerAllocation(int volunteerId, int jobId, string notes)
        {
            VolunteerId = volunteerId;
            JobId = jobId;
            Notes = notes;
        }

        public int VolunteerId { get; private set; }
        public int JobId { get; private set; }
        public string Notes { get; private set; }
    }
}