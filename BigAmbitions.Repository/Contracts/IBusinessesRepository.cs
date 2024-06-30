using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts;

public interface IBusinessesRepository
{
    Task AddAsync(Business business);
    Task RemoveAsync(Business business);
    Task UpdateAsync(Business business);
    ValueTask<Business?> FindAsync(int id);
}
