using Microsoft.EntityFrameworkCore;
using MUSAKA.Domain;

namespace MUSAKA.Data
{
    public class MusakaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<ReceiptOrder> ReceiptsOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=.;Database=MusakaDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderStatus);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cashier);

            modelBuilder.Entity<OrderStatus>()
                .HasKey(os => os.Id);

            modelBuilder.Entity<Receipt>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Cashier);

            modelBuilder.Entity<ReceiptOrder>()
                .HasKey(ro => new {ro.ReceiptId, ro.OrderId});
//            modelBuilder.Entity<ReceiptOrder>()
//                .HasOne(ro => ro.Receipt)
//                .WithOne(r => r.Orders)
//                .HasForeignKey(ro => ro.ReceiptId)
//                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReceiptOrder>()
                .HasOne(ro => ro.Order)
                .WithMany()
                .HasForeignKey(ro => ro.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}