using BigAmbitions.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigAmbitions.Repository.Contexts;
public sealed class BigAmbitionContext : DbContext
{
    public DbSet<BusinessEntity> Businesses { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductConfigEntity> ProductConfigs { get; set; }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigBusinesses(modelBuilder);
        ConfigProducts(modelBuilder);
        ConfigProductConfigs(modelBuilder);

        base.OnModelCreating(modelBuilder);
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

    private static void ConfigProductConfigs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductConfigEntity>(e =>
        {
            ConfigRegister(e);

            e.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            e.HasIndex(i => i.Name).IsDescending(false);
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
}
