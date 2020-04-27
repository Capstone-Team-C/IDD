using System;

namespace AdminUI.Models
{
    public class Lock
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
