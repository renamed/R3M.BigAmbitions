using AutoMapper;
using BigAmbitions.Domain.Config;
using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;
using BigAmbitions.Repository.Entities;

namespace BigAmbitions.Repository;
public class ProductConfigRepository : BaseRepository<ProductConfigEntity, ProductConfig>, IProductConfigRepository
{
    public ProductConfigRepository(BigAmbitionContext dbContext, IMapper mapper) : base(dbContext.ProductConfigs, dbContext, mapper)
    {
    }
}
