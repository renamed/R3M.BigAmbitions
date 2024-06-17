using BigAmbitions.Domain;
using System.Linq.Expressions;

namespace BigAmbitions.Repository.Contracts;
public interface IBusinessRepository
{
    Task AddAsync(Business entity);
    IAsyncEnumerable<Business> ListAsync();
    Task SaveAsync();
}
