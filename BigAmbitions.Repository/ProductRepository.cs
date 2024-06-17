using AutoMapper;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;
using BigAmbitions.Repository.Entities;

namespace BigAmbitions.Repository;
public class ProductRepository : BaseRepository<ProductEntity, Product>, IProductRepository
{
    public ProductRepository(BigAmbitionContext dbContext, IMapper mapper) : base(dbContext.Products, dbContext, mapper)
    {
    }
}
