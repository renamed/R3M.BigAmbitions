using BigAmbitions.Domain;
using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;

namespace BigAmbitions.Repository;

public class GameRepository(BigAmbitionContext bigAmbitionsContext)
    : AbstractRepository<Game>(bigAmbitionsContext), IGameRepository
{
}
