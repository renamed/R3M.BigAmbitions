using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts;
public interface IReportRepository
{
    ValueTask SaveReportAsync(string report);
}
