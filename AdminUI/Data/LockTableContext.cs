using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdminUI.Models;

namespace AdminUI.Data
{
    public class LockTableContext : DbContext
    {
        public LockTableContext(DbContextOptions<LockTableContext> options)
            : base(options)
        {
        }

        public DbSet<LockTableRow> LockTableRow { get; set; }
    }
}
