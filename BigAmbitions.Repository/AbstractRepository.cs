using BigAmbitions.Domain;
using Microsoft.EntityFrameworkCore;

namespace BigAmbitions.Repository;

public class AbstractRepository<T>
    where T : class, IRegister
{
    protected DbContext DbContext { get; init; }

    public AbstractRepository(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public Task AddAsync(T entity)
    {
        DbContext.Add(entity);
        return DbContext.SaveChangesAsync();
    }

    public async ValueTask<T?> FindAsync(int id)
    {
        return (await DbContext.FindAsync(typeof(T), id)) as T;
    }

    public Task RemoveAsync(T entity)
    {
        DbContext.Remove(entity);
        return DbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(T entity)
    {
        DbContext.Update(entity);
        return DbContext.SaveChangesAsync();
    }
}
