using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FormExample.Models;

namespace FormExample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FormExample.Models.SecondaryFinding> SecondaryFinding { get; set; }

        public DbSet<FormExample.Models.PrimaryFinding> PrimaryFinding { get; set; }

        public DbSet<FormExample.Models.Patient> Patient { get; set; }
    }
}
