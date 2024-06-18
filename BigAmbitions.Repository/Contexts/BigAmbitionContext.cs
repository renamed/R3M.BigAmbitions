using BigAmbitions.Repository.Contracts;
using BigAmbitions.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigAmbitions.Repository.Contexts;
public sealed class BigAmbitionContext : DbContext, IBigAmbitionContext
{
    public BigAmbitionContext(){}
    public BigAmbitionContext(DbContextOptions<BigAmbitionContext> options) : base(options) { }
        
    public DbSet<WarehouseEntity> Warehouses { get; set; }
    public DbSet<BusinessEntity> Businesses { get; set; }
    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<WarehouseBusinessEntity> WarehouseBusinesses { get; set; }
    public DbSet<BusinessProductEntity> BusinessProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigWarehouse(modelBuilder);
        ConfigBusinesses(modelBuilder);
        ConfigProducts(modelBuilder);

        ConfigWarehouseBusinesses(modelBuilder);
        ConfigBusinessProducts(modelBuilder);


        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigWarehouse(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WarehouseEntity>(e =>
        {
            ConfigRegister(e);

            e.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            e.HasIndex(i => i.Name).IsDescending(false);
        });
    }

    private static void ConfigBusinesses(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessEntity>(e =>
        {
            ConfigRegister(e);

            e.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            e.HasIndex(i => i.Name).IsDescending(false);
        });
    }

    private static void ConfigProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>(e =>
        {
            ConfigRegister(e);

            e.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            e.HasIndex(i => i.Name).IsDescending(false);
        });
    }

    private static void ConfigWarehouseBusinesses(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WarehouseBusinessEntity>(e =>
        {
            ConfigRegister(e);

            e.HasOne(ho => ho.Warehouse).WithMany().HasForeignKey(ho => ho.WarehouseId);
            e.HasOne(ho => ho.Business).WithMany().HasForeignKey(ho => ho.BusinessId);
        });
    }

    private static void ConfigBusinessProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessProductEntity>(e =>
        {
            ConfigRegister(e);

            e.HasOne(ho => ho.Business).WithMany().HasForeignKey(fk => fk.BusinessId);
            e.HasOne(ho => ho.Product).WithMany().HasForeignKey(fk => fk.ProductId);
        });
    }

    private static EntityTypeBuilder<T> ConfigRegister<T>(EntityTypeBuilder<T> e)
        where T : RegisterEntity
    {
        e.HasKey(i => i.Id);

        e.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        return e;

    }

    public DbContext GetDbContext() => this;    
}
