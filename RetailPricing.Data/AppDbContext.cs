using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using RetailPricing.Models;

namespace RetailPricing.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PricingRecord> PricingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<PricingRecord>(entity =>
            {
                entity.ToTable("PricingRecords");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.StoreId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.SKU).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ProductName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Date).HasColumnType("datetime");

                
            });

          
        }
    }
}
