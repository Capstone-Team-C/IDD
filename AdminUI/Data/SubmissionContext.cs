using AdminUI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminUI.Data
{
    public class SubmissionContext : DbContext
    {
        public SubmissionContext(DbContextOptions<SubmissionContext> options)
            : base(options)
        {
        }

        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<MileageForm> MileageForms { get; set; }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Timesheet>();
            builder.Entity<MileageForm>();

            base.OnModelCreating(builder);
        }*/
    }
}