using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;

namespace BigAmbitions.Repository;
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    protected override string GetFileName() => "products.json";
}
