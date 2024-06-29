using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts;

public interface IGameRepository
{
    Task AddAsync(Game game);
    Task RemoveAsync(Game game);
}
