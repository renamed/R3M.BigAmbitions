using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts
{
    public interface IWarehouseRepository
    {
        Task AddAsync(Warehouse warehouse);
        Task AssignBusinessesAsync(Warehouse warehouse, IEnumerable<Business> businesses);
        IAsyncEnumerable<Warehouse> ListAsync();
    }
}
