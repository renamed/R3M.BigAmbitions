using BigAmbitions.Domain.Config;
using BigAmbitions.Repository.Contracts;

namespace BigAmbitions.Repository;
public class ProductConfigRepository : BaseRepository<ProductConfig>, IProductConfigRepository
{
    protected override string GetFileName()
        => "product_config.json";

    public ProductConfigRepository()
    {
        Refresh(GetFileName());
    }
}
