using BigAmbitions.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace BigAmbitions.Repository.Contracts;

public interface IBigAmbitionContext
{
    public DbSet<WarehouseEntity> Warehouses { get; set; }
    public DbSet<BusinessEntity> Businesses { get; set; }
    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<WarehouseBusinessEntity> WarehouseBusinesses { get; set; }
    public DbSet<BusinessProductEntity> BusinessProducts { get; set; }

    DbContext GetDbContext();
}
