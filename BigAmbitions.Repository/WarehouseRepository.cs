using AutoMapper;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using BigAmbitions.Repository.Entities;

namespace BigAmbitions.Repository;

public class WarehouseRepository : BaseRepository<WarehouseEntity, Warehouse>, IWarehouseRepository
{

    public WarehouseRepository(IBigAmbitionContext dbContext, IMapper mapper) : base(dbContext.Warehouses, dbContext, mapper)
    {
    }

    public Task AssignBusinessesAsync(Warehouse warehouse, IEnumerable<Business> businesses)
    {
        foreach (var business in businesses)
        {
            var currentBusiness = Mapper.Map<BusinessEntity>(business);
            DbContext.WarehouseBusinesses.Add(new WarehouseBusinessEntity
            {
                BusinessId = currentBusiness.Id,
                WarehouseId = warehouse.Id
            });
        }

        return SaveAsync();
    }
}
