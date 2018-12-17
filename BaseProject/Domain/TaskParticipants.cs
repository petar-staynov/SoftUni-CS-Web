namespace TORSHIA_NEW.Models
{
    public class TaskParticipants
    {
        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }
    }
}