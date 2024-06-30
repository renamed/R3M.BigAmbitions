using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts;

public interface IGameRepository
{
    Task AddAsync(Game game);
    Task RemoveAsync(Game game);
    Task UpdateAsync(Game business);
    ValueTask<Game?> FindAsync(int id);
}
