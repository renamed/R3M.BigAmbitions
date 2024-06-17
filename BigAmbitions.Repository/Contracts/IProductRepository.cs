using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts;
public interface IProductRepository
{
    Task AddAsync(Product entity);
}
