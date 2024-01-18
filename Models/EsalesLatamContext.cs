using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Portal_MovilEsales_API.Models;

public partial class EsalesLatamContext : DbContext
{
    public EsalesLatamContext()
    {
    }

    public EsalesLatamContext(DbContextOptions<EsalesLatamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asesore> Asesores { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ImagenesPlanta> ImagenesPlantas { get; set; }

    public virtual DbSet<Planta> Plantas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=10.193.46.9;Database=EsalesLatam;Persist Security Info=False;User ID=admin.sqlec;Password=admin.sqlec;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asesore>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlantaId).HasColumnName("Planta_Id");

            entity.HasOne(d => d.Planta).WithMany(p => p.Asesores)
                .HasForeignKey(d => d.PlantaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asesores_Plantas");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PlantaId).HasColumnName("Planta_Id");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AsesorId).HasColumnName("Asesor_Id");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CodigoSap)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CodigoSAP");
            entity.Property(e => e.Correo)
                .HasMaxLength(528)
                .IsUnicode(false);
            entity.Property(e => e.DireccionContacto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PlantaId).HasColumnName("Planta_Id");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Asesor).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.AsesorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Asesores");

            entity.HasOne(d => d.Planta).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PlantaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Plantas");
        });

        modelBuilder.Entity<ImagenesPlanta>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LinkImagen)
                .HasMaxLength(528)
                .IsUnicode(false);
            entity.Property(e => e.PlantaId).HasColumnName("Planta_Id");

            entity.HasOne(d => d.Planta).WithMany(p => p.ImagenesPlanta)
                .HasForeignKey(d => d.PlantaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImagenesPlantas_Plantas");
        });

        modelBuilder.Entity<Planta>(entity =>
        {
            entity.Property(e => e.CodigoPlanta)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Compania)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DistributionChannel)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Kkber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NombrePlanta)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlantaXproductos)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PlantaXProductos");
            entity.Property(e => e.SalesDocumentType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SalesGroup)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SalesOffice)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SalesOrganization)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SeleccionaFecha)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
