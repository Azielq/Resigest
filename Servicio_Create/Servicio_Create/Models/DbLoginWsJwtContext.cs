using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Servicio_Create.Models;

public partial class DbLoginWsJwtContext : DbContext
{
    public DbLoginWsJwtContext()
    {
    }

    public DbLoginWsJwtContext(DbContextOptions<DbLoginWsJwtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class_Credito> creditos { get; set; }

    public virtual DbSet<Class_MovimientosCredito> movimientosCredito { get; set; }
    public virtual DbSet<Class_Usuarios> personas { get; set; }
    public virtual DbSet<Class_Habitaciones> Habitacion { get; set; }
    public virtual DbSet<Class_Ocupaciones> Ocupacion { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class_Credito>(entity =>
        {
            entity.HasKey(e => e.credito_ID).HasName("PK__tb_Cred__09889210B8E0DAD6");

            entity.ToTable("Creditos");

           
        });

        modelBuilder.Entity<Class_MovimientosCredito>(entity =>
        {
            entity.HasKey(e => e.movimiento_ID).HasName("PK__tb_movcre__5B65BF97730FDED5");

            entity.ToTable("MovimientosCredito");

           
        });
        modelBuilder.Entity<Class_Usuarios>(entity =>
        {
            entity.HasKey(e => e.Cedula_P).HasName("PK__tb_Usu__5B65BF97730FDED5");

            entity.ToTable("Personas");


        });
        modelBuilder.Entity<Class_Habitaciones>(entity =>
        {
            entity.HasKey(e => e.Habitacion_ID).HasName("PK__tb_habi__5B65BF97730FDED5");

            entity.ToTable("Habitaciones");


        });
        modelBuilder.Entity<Class_Ocupaciones>(entity =>
        {
            entity.HasKey(e => e.ocupacion_ID).HasName("PK__tb_ocu__5B65BF97730FDED5");

            entity.ToTable("Ocupaciones");


        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

