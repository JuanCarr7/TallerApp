using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TallerApi.Models;

namespace TallerApi.Data;

public partial class TallerMecanicoPruebaContext : DbContext
{
    public TallerMecanicoPruebaContext()
    {
    }

    public TallerMecanicoPruebaContext(DbContextOptions<TallerMecanicoPruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Unidade> Unidades { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=JUAN-NOTEBOOK;Database=TallerMecanicoPrueba;User Id=JCarreggio;Password=PFxaGtCKQksE3H;Encrypt=false;");

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    IConfiguration configuration = new ConfigurationBuilder()
    //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //        .Build();

    //    var connectionString = configuration.GetConnectionString("TallerMecanicoPrueba");
    //    optionsBuilder.UseSqlServer(connectionString);
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("InMemoryTaller");
        optionsBuilder.UseInMemoryDatabase(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_marcas");

            entity.ToTable("marcas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_productos");

            entity.ToTable("productos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdMarca).HasColumnName("idMarca");
            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");
            entity.Property(e => e.Precio)
                .HasColumnType("money")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.StockMinimo).HasColumnName("stockMinimo");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("fk_productos_marca");

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdUnidad)
                .HasConstraintName("fk_productos_unidad");
        });

        modelBuilder.Entity<Unidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_unidades");

            entity.ToTable("unidades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
