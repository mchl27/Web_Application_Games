using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Application_web_Arquitecture.Models;

public partial class DbWebApplicationContext : DbContext
{
    public DbWebApplicationContext()
    {
    }

    public DbWebApplicationContext(DbContextOptions<DbWebApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Videojuego> Videojuegos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Michael\\SQLEXPRESS;Database=DB_web_application;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Transacc__9B541C388509D4D8");

            entity.Property(e => e.IdTransaccion).HasColumnName("ID_Transaccion");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.IdVideojuego).HasColumnName("Id_Videojuego");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(10)
                .HasColumnName("Tipo_Transaccion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Id_Us__4F7CD00D");

            entity.HasOne(d => d.IdVideojuegoNavigation).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.IdVideojuego)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacci__Id_Vi__5070F446");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__EF59F7629FA99FB3");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534BADCEDDC").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<Videojuego>(entity =>
        {
            entity.HasKey(e => e.IdJuego).HasName("PK__Videojue__DB51539367A4D3AB");

            entity.Property(e => e.IdJuego).HasColumnName("Id_juego");
            entity.Property(e => e.Genero).HasMaxLength(50);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Titulo).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
