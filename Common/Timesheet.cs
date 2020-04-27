using System.Collections.Generic;

namespace AdminUI.Models
{
    public class Timesheet : Submission
    {
        public double TotalHours { get; set; }
        public ICollection<TimeEntry> TimeEntries { get; set; }
    }
}
