using System;
using Microsoft.EntityFrameworkCore;
using TORSHIA_NEW.Models;

namespace TORSHIA_NEW.Data
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connection.ConnectionConfig());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TaskSectors>().HasKey(ts => new {ts.TaskId, ts.SectorId});
            //modelBuilder.Entity<TaskParticipants>().HasKey(tp => new {tp.TaskId, tp.ParticipantId});

            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<User> Users { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        //public DbSet<TaskSectors> TasksSectors { get; set; }
    }
}