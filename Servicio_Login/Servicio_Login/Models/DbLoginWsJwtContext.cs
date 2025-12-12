using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Servicio_Login.Models;

namespace Servicio_Login.Models;

public partial class DbLoginWsJwtContext : DbContext
{
    public DbLoginWsJwtContext()
    {
    }

    public DbLoginWsJwtContext(DbContextOptions<DbLoginWsJwtContext> options)
        : base(options)
    {
    }

   

    public virtual DbSet<Class_Usuario> TbUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Class_Usuario>(entity =>
        {
            entity.HasKey(e => e.Cedula_P).HasName("PK__tb_Usuar__5B65BF97730FDED5");

            entity.ToTable("Personas");

            entity.Property(e => e.Contrasenna_P)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo_P)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre_P)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

