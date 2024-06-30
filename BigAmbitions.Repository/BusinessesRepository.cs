using BigAmbitions.Domain;
using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;

namespace BigAmbitions.Repository;

public class BusinessesRepository(BigAmbitionContext bigAmbitionsContext) 
    : AbstractRepository<Business>(bigAmbitionsContext), IBusinessesRepository
{
}
