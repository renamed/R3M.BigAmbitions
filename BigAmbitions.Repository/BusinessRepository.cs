using AutoMapper;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;
using BigAmbitions.Repository.Entities;

namespace BigAmbitions.Repository;
public class BusinessRepository : BaseRepository<BusinessEntity, Business>, IBusinessRepository
{
    public BusinessRepository(IBigAmbitionContext dbContext, IMapper mapper) : base(dbContext.Businesses, dbContext, mapper)
    {
    }

    public Task AssignProductsAsync(Business business, IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            var currentProduct = Mapper.Map<ProductEntity>(product);
            DbContext.BusinessProducts.Add(new BusinessProductEntity
            {
                ProductId = currentProduct.Id,
                BusinessId = business.Id
            });
        }

        return SaveAsync();
    }
}
