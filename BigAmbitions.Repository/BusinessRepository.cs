using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;

namespace BigAmbitions.Repository;
public class BusinessRepository : BaseRepository<Business>, IBusinessRepository
{
    protected override string GetFileName() => "businesses.json";

    public ValueTask SaveAsync()
    {
        return SaveAsync(GetFileName(), EntityList);
    }
}
