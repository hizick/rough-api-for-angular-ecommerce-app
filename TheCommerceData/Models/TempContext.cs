using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheCommerceData.Models
{
    public partial class TempContext : DbContext
    {
        public TempContext()
        {
        }

        public TempContext(DbContextOptions<TempContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ISAAC;Database=MrCommerceDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => new { e.ShoppingCartId, e.ProductId, e.ProductName, e.CategoryId })
                    .HasName("PK_ShoppingCart_1");

                entity.Property(e => e.ShoppingCartId)
                    .HasColumnName("ShoppingCartID")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");
            });
        }
    }
}
