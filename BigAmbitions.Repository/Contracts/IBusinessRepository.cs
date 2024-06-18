using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts;
public interface IBusinessRepository
{
    Task AddAsync(Business entity);
    IAsyncEnumerable<Business> ListAsync();
    Task SaveAsync();
}
