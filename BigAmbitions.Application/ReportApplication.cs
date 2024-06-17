using BigAmbitions.Application.Contracts;
using BigAmbitions.Application.Extensions;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using System.Text;
using System.Text.Json;

namespace BigAmbitions.Application;
public class ReportApplication : IReportApplication
{
    private const string OutDirectory = "out";

    private readonly IBusinessRepository businessRepository;

    public ReportApplication(IBusinessRepository businessRepository)
    {
        this.businessRepository = businessRepository;
    }

    public async ValueTask GenerateReportAsync()
    {
        var warehouse = new Warehouse();
        warehouse.Businesses = await businessRepository.ListAsync().ToListAsync();

        Path.Combine(Environment.CurrentDirectory, OutDirectory);

        await File.WriteAllTextAsync(Path.Combine(OutDirectory, $"report_{DateTime.Now:yyyyMMddHHmmss}.txt"),
            JsonSerializer.Serialize(warehouse, new JsonSerializerOptions { WriteIndented = true }));
    }
}
