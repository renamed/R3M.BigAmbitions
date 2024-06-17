using AutoMapper;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BigAmbitions.Repository;
public abstract class BaseRepository<TEntity, TDomain>
    where TEntity : class
{
    protected BaseRepository(DbSet<TEntity> entityList, DbContext dbContext, IMapper mapper)
    {
        EntityList = entityList;
        DbContext = dbContext;
        Mapper = mapper;
    }

    protected readonly DbSet<TEntity> EntityList;
    protected readonly DbContext DbContext;
    protected readonly IMapper Mapper;

    public Task AddAsync(TDomain domain)
    {
        var entity = Mapper.Map<TEntity>(domain);
        EntityList.Add(entity);
        return SaveAsync();
    }

    public async Task<TDomain?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var response = await EntityList.FirstOrDefaultAsync(predicate);
        return Mapper.Map<TDomain?>(response);
    }

    public async IAsyncEnumerable<TDomain> ListAsync()
    {
        await foreach(var currentEntity in EntityList.AsAsyncEnumerable())
        {
            yield return Mapper.Map<TDomain>(currentEntity);
        }
    }

    public Task SaveAsync()
    {
        return DbContext.SaveChangesAsync();
    }
}
