using BigAmbitions.Domain;
using BigAmbitions.Domain.Config;

namespace BigAmbitions.Repository.Contracts;
public interface IProductConfigRepository
{
    Task AddAsync(ProductConfig entity);
    IAsyncEnumerable<ProductConfig> ListAsync();
}
