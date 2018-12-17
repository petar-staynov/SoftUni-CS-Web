﻿using System;

namespace TORSHIA.Domain
{
    public class Report
    {
        /*
           •	Has an Id – a GUID String or an Integer.
           •	Has a Status – can be one of the following values (“Completed”, “Archived”).
           •	Has a Reported On – a Date object.
           •	Has a Task – a Task object.
           •	Has a Reporter – an User object.
         */

        public int Id { get; set; }
        public ReportStatus Status { get; set; }
        public DateTime ReportedOn { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int ReporterId { get; set; }
        public User Reporter { get; set; }
    }
}