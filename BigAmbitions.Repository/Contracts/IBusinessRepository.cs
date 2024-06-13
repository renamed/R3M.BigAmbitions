using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts;
public interface IBusinessRepository
{
    ValueTask AddAsync(Business entity);
    ValueTask<IReadOnlyList<Business>> ListAsync();
    ValueTask<Business?> GetAsync(Func<Business, bool> predicate);
    ValueTask SaveAsync();
}
