using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdminUI.Models;

namespace AdminUI.Data
{
    public class TimesheetContext : DbContext
    {
        public TimesheetContext (DbContextOptions<TimesheetContext> options)
            : base(options)
        {
        }

        public DbSet<Timesheet> Timesheet { get; set; }
    }
}
