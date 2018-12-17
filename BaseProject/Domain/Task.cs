using System;
using System.Collections;
using System.Collections.Generic;

namespace TORSHIA_NEW.Models
{
    public class Task
    {
        /*
         •	Has an Id – a GUID String or an Integer.
           •	Has a Title
           •	Has a Due Date – a Date object (by default – false).
           •	Has a IsReported – a boolean. 
           •	Has a Description
           •	Has Participants – (entered as comma separated string values... You can store them however you want)
           •	Has Affected Sectors – a collection which may contain 1 or more of 
           the following values (“Customers”, “Marketing”, “Finances”, “Internal”, “Management”)
         */
        public Task()
        {
            this.Participants = new List<TaskParticipants>();
            this.AffectedSectors = new List<TaskSectors>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsReported { get; set; }
        public string Description { get; set; }

        public ICollection<TaskParticipants> Participants { get; set; }
        public ICollection<TaskSectors> AffectedSectors { get; set; }
    }
}