using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.models;

public partial class DelicatessenProjectContext : DbContext
{
    public DelicatessenProjectContext()
    {
    }

    public DelicatessenProjectContext(DbContextOptions<DelicatessenProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<HighQuantity> HighQuantities { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=OshritDjavsarov;Database=DelicatessenProject;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BD9E1CA25");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0380C1D3E").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(200);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D86A1ED854");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534118CCFAC").IsUnique();

            entity.Property(e => e.AddressStreet).HasMaxLength(300);
            entity.Property(e => e.Apartment)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.BuildEntry)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.BuildFloor)
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.City).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.LastName).HasMaxLength(200);
            entity.Property(e => e.PasswordHash).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.PrivateName).HasMaxLength(200);
        });

        modelBuilder.Entity<HighQuantity>(entity =>
        {
            entity.HasKey(e => e.Hqid).HasName("PK__HighQuan__1437542C2E4C9514");

            entity.ToTable("HighQuantity");

            entity.Property(e => e.Hqid).HasColumnName("HQId");

            entity.HasOne(d => d.Product).WithMany(p => p.HighQuantities)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HighQuant__Produ__403A8C7D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFDE5064E2");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Customer__4AB81AF0");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED0681F7F3A426");

            entity.Property(e => e.OrderItemPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__4D94879B");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Produ__4E88ABD4");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CDA59D1FDE");

            entity.Property(e => e.FilterProduct).HasMaxLength(100);
            entity.Property(e => e.HeatingInstruction).HasMaxLength(300);
            entity.Property(e => e.ImageUrl).HasMaxLength(300);
            entity.Property(e => e.PriceDescription).HasMaxLength(300);
            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
