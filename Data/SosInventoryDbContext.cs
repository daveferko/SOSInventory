using Microsoft.EntityFrameworkCore;
using SOSInventory.Models;

namespace SOSInventory.Data
{
    public class SosInventoryDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemReceipt> ItemReceipts { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Adjustment> Adjustments { get; set; }

        public SosInventoryDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasKey(p => p.ItemId);
            modelBuilder.Entity<ItemReceipt>()
                .HasKey(p => p.ItemReceiptId);
            modelBuilder.Entity<ItemReceipt>()
                .HasOne<Item>()
                .WithMany()
                .HasForeignKey(ir => ir.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Shipment>()
                .HasKey(p => p.ShipmentId);
            modelBuilder.Entity<Shipment>()
                .HasOne<Item>()
                .WithMany()
                .HasForeignKey(s => s.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Adjustment>()
                .HasKey(p => p.AdjustmentId);
            modelBuilder.Entity<Adjustment>()
               .HasOne<Item>()
               .WithMany()
               .HasForeignKey(a => a.ItemId)
               .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
