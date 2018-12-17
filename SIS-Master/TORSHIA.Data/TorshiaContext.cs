using System;
using Microsoft.EntityFrameworkCore;
using TORSHIA.Domain;

namespace TORSHIA.Data
{
    public class TorshiaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connection.ConnectionConfig());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Report> Reports { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SectorType> SectorTypes { get; set; }
        public DbSet<ReportStatus> ReportStatuses { get; set; }

    }
}