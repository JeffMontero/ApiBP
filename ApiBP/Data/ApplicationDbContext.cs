using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiBP.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cuentum> Cuenta { get; set; } = null!;
        public virtual DbSet<Movimiento> Movimientos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.0.2.66;Database=BPDb;user id=sql_service;password=GrupoL@@r2015;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__D5946642060DEAE8");

                entity.ToTable("Cliente");

                entity.Property(e => e.Clave).HasMaxLength(20);

                entity.Property(e => e.Estado).HasMaxLength(1);

                entity.HasOne(d => d.PersonaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Persona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cliente__Persona__07F6335A");
            });

            modelBuilder.Entity<Cuentum>(entity =>
            {
                entity.HasKey(e => e.IdCuenta)
                    .HasName("PK__Cuenta__D41FD7060AD2A005");

                entity.HasIndex(e => e.NumeroCuenta, "UQ__Cuenta__E039507B0DAF0CB0")
                    .IsUnique();

                entity.Property(e => e.Estado).HasMaxLength(1);

                entity.Property(e => e.NumeroCuenta).HasMaxLength(10);

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(12, 3)");

                entity.Property(e => e.TipoCuenta).HasMaxLength(1);

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cuenta__Cliente__0F975522");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento)
                    .HasName("PK__Movimien__881A6AE01273C1CD");

                entity.Property(e => e.Estado).HasMaxLength(1);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.MovDescripcion).HasMaxLength(200);

                entity.Property(e => e.Saldo).HasColumnType("decimal(12, 3)");

                entity.Property(e => e.TipoMovimiento).HasMaxLength(1);

                entity.Property(e => e.Valor).HasColumnType("decimal(12, 3)");

                entity.HasOne(d => d.CuentaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.Cuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__Cuent__145C0A3F");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona)
                    .HasName("PK__Persona__2EC8D2AC7F60ED59");

                entity.ToTable("Persona");

                entity.HasIndex(e => e.Identificacion, "UQ__Persona__D6F931E5023D5A04")
                    .IsUnique();

                entity.Property(e => e.Direccion).HasMaxLength(200);

                entity.Property(e => e.Genero).HasMaxLength(1);

                entity.Property(e => e.Identificacion).HasMaxLength(10);

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Telefono).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
