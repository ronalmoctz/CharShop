using System;
using System.Collections.Generic;
using CharShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CharShop.Data;

public partial class CarShopDbContext : DbContext
{
    public CarShopDbContext()
    {
    }

    public CarShopDbContext(DbContextOptions<CarShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarTitle> CarTitles { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<OauthProvider> OauthProviders { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserOauth> UserOauths { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0342E75F4B706");

            entity.Property(e => e.CarId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
        });

        modelBuilder.Entity<CarTitle>(entity =>
        {
            entity.HasKey(e => e.TitleId).HasName("PK__CarTitle__75758986B62F05C6");

            entity.Property(e => e.TitleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IssuedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Sale).WithMany(p => p.CarTitles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarTitles__SaleI__693CA210");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAB5AC3F3CD4");

            entity.Property(e => e.InvoiceId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.GeneratedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Sale).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoices__SaleId__6383C8BA");
        });

        modelBuilder.Entity<OauthProvider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PK__OAuthPro__B54C687D022A4660");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1D3C0C8F723");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42FCF5DCEE1D1");

            entity.Property(e => e.PromotionId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Car).WithMany(p => p.Promotions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promotion__CarId__571DF1D5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AEAA39BE6");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C3FFE53882E2");

            entity.Property(e => e.SaleId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SaleDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Car).WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__CarId__5BE2A6F2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__CustomerI__5CD6CB2B");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Sales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sales__PaymentMe__5DCAEF64");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CE8835CE5");

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__3F466844");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailId).HasName("PK__UserDeta__564F56B27B871356");

            entity.Property(e => e.UserDetailId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserDetai__UserI__4316F928");
        });

        modelBuilder.Entity<UserOauth>(entity =>
        {
            entity.HasKey(e => e.UserOauthId).HasName("PK__UserOAut__09B8402298454229");

            entity.Property(e => e.UserOauthId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Provider).WithMany(p => p.UserOauths)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserOAuth__Provi__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.UserOauths)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserOAuth__UserI__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
