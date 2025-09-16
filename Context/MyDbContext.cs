using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UGTest.Migrations; 
namespace UGTest.Context;


public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MusteriFaturaTable> MusteriFaturaTables { get; set; }

    public virtual DbSet<MusteriTanimTable> MusteriTanimTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ug;User Id=sa;Password=Tk123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MusteriFaturaTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__musteri___3214EC2728F9FF75");

            entity.ToTable("musteri_fatura_table");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FaturaTarihi).HasColumnName("FATURA_TARIHI");
            entity.Property(e => e.FaturaTutari)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("FATURA_TUTARI");
            entity.Property(e => e.MusteriId).HasColumnName("MUSTERI_ID");
            entity.Property(e => e.OdemeTarihi).HasColumnName("ODEME_TARIHI");
        });

        modelBuilder.Entity<MusteriTanimTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__musteri___3214EC277AA69E36");

            entity.ToTable("musteri_tanim_table");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Unvan)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("UNVAN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
