using BigAmbitions.Domain;

namespace BigAmbitions.Application.Contracts;

public interface IBusinessesApplication
{
    Task<Business> AddAsync(Business business);
    Task<Business> EditAsync(int id, Business business);
    ValueTask<Business?> FindAsync(int id);
    Task RemoveAsync(int id);
}