using BigAmbitions.Repository.Contracts;

namespace BigAmbitions.Repository;

public class ReportRepository : IReportRepository
{
    public ValueTask SaveReportAsync(string report)
    {
        return new ValueTask(File.WriteAllTextAsync(Path.Combine("out", $"report_{DateTime.Now:yyyyMMddHHmmss}.txt"), report));
    }
}
