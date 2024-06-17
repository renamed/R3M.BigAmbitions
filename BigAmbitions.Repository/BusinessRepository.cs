using AutoMapper;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;
using BigAmbitions.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BigAmbitions.Repository;
public class BusinessRepository : BaseRepository<BusinessEntity, Business>, IBusinessRepository
{
    public BusinessRepository(BigAmbitionContext dbContext, IMapper mapper) : base(dbContext.Businesses, dbContext, mapper)
    {
    }
}
