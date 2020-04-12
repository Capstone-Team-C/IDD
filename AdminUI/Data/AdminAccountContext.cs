using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminUI.Data
{
    public class AdminAccountContext : IdentityDbContext
    {
        public AdminAccountContext(DbContextOptions<AdminAccountContext> options)
            : base(options)
        {
        }
    }
}
