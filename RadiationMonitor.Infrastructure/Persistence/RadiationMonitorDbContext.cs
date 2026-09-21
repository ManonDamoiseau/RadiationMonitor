using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RadiationMonitor.Domain.Entities;

namespace RadiationMonitor.Infrastructure.Persistence
{
    public class RadiationMonitorDbContext : DbContext
    {
        public RadiationMonitorDbContext(
            DbContextOptions<RadiationMonitorDbContext> options) 
            : base(options) 
        { 
        }
        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Measurement>()
                .Property(x => x.Status)
                .HasConversion<string>();
        }
    }
}
