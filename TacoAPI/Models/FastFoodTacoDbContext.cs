using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TacoAPI.Models;

public partial class FastFoodTacoDbContext : DbContext
{
    public FastFoodTacoDbContext()
    {
    }

    public FastFoodTacoDbContext(DbContextOptions<FastFoodTacoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<Taco> Tacos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=FastFoodTacoDB; Integrated Security=SSPI;Encrypt=false;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drink__3214EC27BC8486EE");

            entity.ToTable("Drink");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Taco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Taco__3214EC279F2CDF6D");

            entity.ToTable("Taco");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC271BF52455");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApiKey).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
