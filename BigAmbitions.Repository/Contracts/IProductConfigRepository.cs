using BigAmbitions.Domain;
using BigAmbitions.Domain.Config;

namespace BigAmbitions.Repository.Contracts;
public interface IProductConfigRepository
{
    ValueTask AddAsync(ProductConfig entity);
    ValueTask<ProductConfig?> GetAsync(Func<ProductConfig, bool> predicate);
    ValueTask<IReadOnlyList<ProductConfig>> ListAsync();
}
