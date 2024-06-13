using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts;
public interface IProductRepository
{
    ValueTask AddAsync(Product entity);
    ValueTask<Product?> GetAsync(Func<Product, bool> predicate);
}
