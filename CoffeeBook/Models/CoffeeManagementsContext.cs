using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBook.Models;

public partial class CoffeeManagementsContext : DbContext
{
    public CoffeeManagementsContext()
    {
    }

    public CoffeeManagementsContext(DbContextOptions<CoffeeManagementsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillInfo> BillInfos { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;database=CoffeeManagements;Integrated security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC07DD7815FC");

            entity.ToTable("Account");

            entity.HasIndex(e => e.UserName, "UQ__Account__C9F28456842E072B").IsUnique();

            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'N/A')");
            entity.Property(e => e.PassWord)
                .IsRequired()
                .HasMaxLength(200)
                .HasDefaultValueSql("((0))");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3214EC07B13088DD");

            entity.ToTable("Bill");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_by");
            entity.Property(e => e.DateCheckIn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.DateCheckOut).HasColumnType("date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Bill__Created_by__52593CB8");

            entity.HasOne(d => d.IdTableNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdTable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__IdTable__5165187F");
        });

        modelBuilder.Entity<BillInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BillInfo__3214EC07AF03C279");

            entity.ToTable("BillInfo");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.IdBill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillInfo__IdBill__5629CD9C");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillInfo__IdProd__571DF1D5");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07E2F422EE");

            entity.ToTable("Category");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'N/A')");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07FCC6648B");

            entity.ToTable("Product");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'N/A')");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__IdCateg__2C3393D0");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Table__3214EC076D62E8C4");

            entity.ToTable("Table");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValueSql("(N'N/A')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
