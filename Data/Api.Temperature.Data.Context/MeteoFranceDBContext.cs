using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Api.Temperature.Data.Entity.Model;
using Api.Temperature.Data.Context.Contract;

namespace Api.Temperature.Data.Context
{
    public partial class MeteoFranceDBContext : DbContext , IMeteoFranceDBContext
    {
        public MeteoFranceDBContext()
        {
        }

        public MeteoFranceDBContext(DbContextOptions<MeteoFranceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departement> Departements { get; set; } = null!;
        public virtual DbSet<Mesure> Mesures { get; set; } = null!;
        public virtual DbSet<Entity.Model.Temperature> Temperatures { get; set; } = null!;
        public virtual DbSet<Unite> Unites { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Departement>(entity =>
            {
                entity.HasKey(e => e.IdDepartement)
                    .HasName("PRIMARY");

                entity.ToTable("departement");

                entity.Property(e => e.IdDepartement).HasColumnName("id_departement");

                entity.Property(e => e.CodeDepartement)
                    .HasMaxLength(50)
                    .HasColumnName("code_departement");

                entity.Property(e => e.NomDepartement)
                    .HasMaxLength(50)
                    .HasColumnName("nom_departement");
            });

            modelBuilder.Entity<Mesure>(entity =>
            {
                entity.HasKey(e => e.IdMesure)
                    .HasName("PRIMARY");

                entity.ToTable("mesure");

                entity.HasIndex(e => e.IdTemperature, "id_temperature");

                entity.HasIndex(e => e.IdUnite, "id_unite");

                entity.Property(e => e.IdMesure).HasColumnName("id_mesure");

                entity.Property(e => e.IdTemperature).HasColumnName("id_temperature");

                entity.Property(e => e.IdUnite).HasColumnName("id_unite");

                entity.Property(e => e.Valeur).HasColumnName("valeur");

                entity.HasOne(d => d.IdTemperatureNavigation)
                    .WithMany(p => p.Mesures)
                    .HasForeignKey(d => d.IdTemperature)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mesure_ibfk_2");

                entity.HasOne(d => d.IdUniteNavigation)
                    .WithMany(p => p.Mesures)
                    .HasForeignKey(d => d.IdUnite)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mesure_ibfk_1");
            });

            modelBuilder.Entity<Entity.Model.Temperature>(entity =>
            {
                entity.HasKey(e => e.IdTemperature)
                    .HasName("PRIMARY");

                entity.ToTable("temperature");

                entity.HasIndex(e => e.IdDepartement, "id_departement");

                entity.Property(e => e.IdTemperature).HasColumnName("id_temperature");

                entity.Property(e => e.DatePriseTemperature)
                    .HasColumnType("datetime")
                    .HasColumnName("date_prise_temperature");

                entity.Property(e => e.IdDepartement).HasColumnName("id_departement");

                entity.HasOne(d => d.IdDepartementNavigation)
                    .WithMany(p => p.Temperatures)
                    .HasForeignKey(d => d.IdDepartement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("temperature_ibfk_1");
            });

            modelBuilder.Entity<Unite>(entity =>
            {
                entity.HasKey(e => e.IdUnite)
                    .HasName("PRIMARY");

                entity.ToTable("unite");

                entity.Property(e => e.IdUnite).HasColumnName("id_unite");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
