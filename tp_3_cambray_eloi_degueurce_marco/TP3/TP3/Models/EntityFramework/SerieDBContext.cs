using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace TP3.Models.EntityFramework;

public partial class SerieDBContext : DbContext
{
    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        
    public SerieDBContext()
    {
    }

    public SerieDBContext(DbContextOptions<SerieDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Notation> Notations { get; set; } = null!;
    public virtual DbSet<Serie> Series { get; set; } = null!;
    public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;

/* 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=FilmsDB;uid=postgres;password=postgres;");
            optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseNpgsql("Server=localhost;port=5432;Database=FilmsDB;uid=postgres;password=postgres;");
        }
    }
*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notation>(entity =>
        {
            entity.HasKey(e => new { e.SerieId, e.UtilisateurId })
                .HasName("pk_notation");

            entity.HasOne(d => d.SerieNotee)
                .WithMany(p => p.NotesSeries)
                .HasForeignKey(d => d.SerieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notes_serie");

            entity.HasOne(d => d.UtilisateurNotant)
                .WithMany(p => p.NotesUtilisateur)
                .HasForeignKey(d => d.UtilisateurId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notes_utilisateur");
            entity.HasCheckConstraint("ck_not_note", "not_note between 0 and 5");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.Property(u => u.Pays).HasDefaultValue("France");
            entity.Property(u => u.DateCreation).HasColumnType("Date");
            entity.Property(u => u.DateCreation).HasDefaultValueSql("now()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
