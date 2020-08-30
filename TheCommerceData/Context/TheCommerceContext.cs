using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheCommerceData.Models
{
    public partial class TheCommerceContext : DbContext
    {

        public TheCommerceContext(DbContextOptions<TheCommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDetails> AccountDetails { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=ISAAC;Database=MrCommerceDB;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AccountDetails>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasMaxLength(37)
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryDescription).HasMaxLength(37);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(37);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CategoryId, e.ProductName });

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(50);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => new { e.ShoppingCartId, e.ProductId})
                    .HasName("PK_ShoppingCart_1");

                entity.Property(e => e.ShoppingCartId)
                    .HasColumnName("ShoppingCartID")
                    .HasMaxLength(50);

                //entity.Property(e => e.ProductId)
                //    .HasColumnName("ProductID")
                //    .HasMaxLength(50);

                //entity.Property(e => e.ProductName).HasMaxLength(50);

                //entity.Property(e => e.CategoryId)
                //    .HasColumnName("CategoryID")
                //    .HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");
                //entity.HasMany(e => e.Products)
                //.WithOne();
            });
        }
    }
}
