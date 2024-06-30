using BigAmbitions.Domain;

namespace BigAmbitions.Application.Contracts;

public interface IGameApplication
{
    Task<Game> AddAsync(Game game);
    Task<Game> EditAsync(int id, Game game);
    ValueTask<Game?> FindAsync(int id);
    Task RemoveAsync(int id);
}