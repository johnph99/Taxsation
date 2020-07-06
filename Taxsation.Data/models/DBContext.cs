using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Taxsation.Data.models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PostalCodeRates> PostalCodeRates { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<TaxRates> TaxRates { get; set; }
        public virtual DbSet<TaxTypes> TaxTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\John\\source\\repos\\Taxsation\\Database\\Taxsation.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCodeRates>(entity =>
            {
                entity.HasKey(e => e.PostalCodeId)
                    .HasName("PK__PostalCo__E197FE4144744014");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.PostalCodeRates)
                    .HasForeignKey(d => d.TaxTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostalCodeRates_TaxTypes");
            });

            modelBuilder.Entity<Requests>(entity =>
            {
                entity.Property(e => e.CalculatedTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequestZipCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TaxRates>(entity =>
            {
                entity.HasKey(e => e.RateIdId)
                    .HasName("PK__TaxRates__CA8D36B7208106F9");

                entity.Property(e => e.FlatValue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Rate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.UpperLimit).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.TaxType)
                    .WithMany(p => p.TaxRates)
                    .HasForeignKey(d => d.TaxTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxRates_TaxTypes");
            });

            modelBuilder.Entity<TaxTypes>(entity =>
            {
                entity.HasKey(e => e.TaxTypeId)
                    .HasName("PK__tmp_ms_x__B5343F436CD2DC07");

                entity.Property(e => e.TaxTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
