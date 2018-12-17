using System.Security.Principal;

namespace TORSHIA_NEW.Models
{
    public class TaskSectors
    {
        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int SectorId { get; set; }
        public SectorType Sector { get; set; }
    }
}