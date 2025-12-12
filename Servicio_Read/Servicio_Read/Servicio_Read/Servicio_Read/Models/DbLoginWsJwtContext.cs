using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Servicio_Read.Models;

namespace Servicio_Read.Models;

public partial class DbLoginWsJwtContext : DbContext
{
    public DbLoginWsJwtContext()
    {
    }

    public DbLoginWsJwtContext(DbContextOptions<DbLoginWsJwtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Credito> creditos { get; set; }

    public virtual DbSet<MovimientosCredito> movimientosCredito { get; set; }
    public virtual DbSet<Persona> personas { get; set; }
    public virtual DbSet<Habitacione> habitaciones { get; set; }
    public virtual DbSet<Ocupacione> ocupaciones { get; set; }
    public virtual DbSet<Role> roles { get; set; }
    public virtual DbSet<TiposHabitacion> tipohabitacion { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Credito>(entity =>
        {
            entity.HasKey(e => e.credito_ID).HasName("PK__tb_Cred__09889210B8E0DAD6");

            entity.ToTable("Creditos");


        });

        modelBuilder.Entity<MovimientosCredito>(entity =>
        {
            entity.HasKey(e => e.movimiento_ID).HasName("PK__tb_movcre__5B65BF97730FDED5");

            entity.ToTable("MovimientosCredito");


        });
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Cedula_P).HasName("PK__tb_Usu__5B65BF97730FDED5");

            entity.ToTable("Personas");


        });
        modelBuilder.Entity<Habitacione>(entity =>
        {
            //entity.HasKey(e => e.Habitacion_ID).HasName("PK__tb_habi__5B65BF97730FDED5");
            entity.HasKey(e => e.habitacion_ID).HasName("PK__tb_habi__5B65BF97730FDED5");

            entity.ToTable("Habitaciones");


        });
        modelBuilder.Entity<Ocupacione>(entity =>
        {
            entity.HasKey(e => e.ocupacion_ID).HasName("PK__tb_ocu__5B65BF97730FDED5");

            entity.ToTable("Ocupaciones");


        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.rol_ID).HasName("PK__tb_Role__5B65BF97730FDED5");

            entity.ToTable("Roles");


        });
        modelBuilder.Entity<TiposHabitacion>(entity =>
        {
            entity.HasKey(e => e.tipo_ID).HasName("PK__tb_Tipo__5B65BF97730FDED5");

            entity.ToTable("TiposHabitacion");


        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

