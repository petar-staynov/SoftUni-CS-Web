using System;

namespace TORSHIA.Common.Models.View
{
    public class CreateTaskBindingModel
    {
        public string title { get; set; }
        public string dueDate { get; set; }
        public string description { get; set; }
        public string participants { get; set; }
        //public string AffectedSectors { get; set; }
    }
}