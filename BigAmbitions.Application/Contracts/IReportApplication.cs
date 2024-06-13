using BigAmbitions.Domain;

namespace BigAmbitions.Application.Contracts;
public interface IReportApplication
{
    ValueTask GenerateReportAsync();
}
