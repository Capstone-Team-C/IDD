using System;
using System.ComponentModel.DataAnnotations;

namespace AdminUI.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool Group { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Time)]
        public DateTime In { get; set; }
        [DataType(DataType.Time)]
        public DateTime Out { get; set; }
        public double Hours { get; set; }

    }
}
