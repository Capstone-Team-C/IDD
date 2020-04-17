using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdminUI.Models
{
    public class LockTableRow
    {
        public int Id { get; set; }
        public int TimesheetID { get; set; }
        public string User { get; set; }
        public DateTime TimeLocked { get; set; }
        public DateTime LastInteraction { get; set; }

    }
}
