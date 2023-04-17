using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AnnonsAPIgood.Models;

public partial class Db1Context : DbContext
{
    public Db1Context()
    {
    }

    public Db1Context(DbContextOptions<Db1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Ad> TblAds { get; set; }

    public virtual DbSet<Adress> TblAdresses { get; set; }

    public virtual DbSet<Annonsorer> TblAnnonsorers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ad>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__tbl.Ads__CAA5BA0F92B8EB96");

            entity.ToTable("tbl.Ads");

            entity.Property(e => e.AdId)
                .ValueGeneratedNever()
                .HasColumnName("ad_Id");
            entity.Property(e => e.AdAnnonsor).HasColumnName("ad_Annonsor");
            entity.Property(e => e.AdAnnonspris).HasColumnName("ad_Annonspris");
            entity.Property(e => e.AdInnehåll).HasColumnName("ad_Innehåll");
            entity.Property(e => e.AdPris).HasColumnName("ad_pris");
            entity.Property(e => e.AdRubrik)
                .HasMaxLength(50)
                .HasColumnName("ad_Rubrik");

            entity.HasOne(d => d.AdAnnonsorNavigation).WithMany(p => p.TblAds)
                .HasForeignKey(d => d.AdAnnonsor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl.Ads__ad_Anno__286302EC");
        });

        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.AdrId).HasName("PK__tbl_Adre__055505673F5F4266");

            entity.ToTable("tbl_Adress");

            entity.Property(e => e.AdrId)
                .ValueGeneratedNever()
                .HasColumnName("adr_Id");
            entity.Property(e => e.AdrAnnonsor).HasColumnName("adr_Annonsor");
            entity.Property(e => e.AdrGata)
                .HasMaxLength(50)
                .HasColumnName("adr_Gata");
            entity.Property(e => e.AdrOrt)
                .HasMaxLength(50)
                .HasColumnName("adr_Ort");
            entity.Property(e => e.AdrPostnummer).HasColumnName("adr_Postnummer");

            entity.HasOne(d => d.AdrAnnonsorNavigation).WithMany(p => p.TblAdresses)
                .HasForeignKey(d => d.AdrAnnonsor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbl_Adres__adr_A__29572725");
        });

        modelBuilder.Entity<Annonsorer>(entity =>
        {
            entity.HasKey(e => e.AnId).HasName("PK__tbl_Anno__831CAFDB334C0E3E");

            entity.ToTable("tbl_Annonsorer");

            entity.Property(e => e.AnId)
                .ValueGeneratedNever()
                .HasColumnName("an_Id");
            entity.Property(e => e.AnFakturaadress).HasColumnName("an_Fakturaadress");
            entity.Property(e => e.AnNamn)
                .HasMaxLength(50)
                .HasColumnName("an_Namn");
            entity.Property(e => e.AnOrgnr).HasColumnName("an_Orgnr");
            entity.Property(e => e.AnSubnr).HasColumnName("an_Subnr");
            entity.Property(e => e.AnTele).HasColumnName("an_Tele");
            entity.Property(e => e.AnUtdadress).HasColumnName("an_Utdadress");

            entity.HasOne(d => d.AnFakturaadressNavigation).WithMany(p => p.TblAnnonsorerAnFakturaadressNavigations)
                .HasForeignKey(d => d.AnFakturaadress)
                .HasConstraintName("FK__tbl_Annon__an_Fa__2A4B4B5E");

            entity.HasOne(d => d.AnUtdadressNavigation).WithMany(p => p.TblAnnonsorerAnUtdadressNavigations)
                .HasForeignKey(d => d.AnUtdadress)
                .HasConstraintName("FK__tbl_Annon__an_Ut__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
