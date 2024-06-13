using BigAmbitions.Application.Contracts;
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
        //var warehouse = new Warehouse();
        //var sb = new StringBuilder();

        //warehouse.Businesses = new List<Business>(await businessRepository.ListAsync());

        //sb.AppendLine("====================================================");
        //sb.AppendLine("Daily buy - by Business");
        //sb.AppendLine("====================================================");
        //foreach (var business in warehouse.Businesses)
        //{
        //    sb.Append(business.Name);
        //    foreach (var product in business.Products.OrderBy(x => x.Name))
        //    {
        //        sb.Append(product.Name);
        //        sb.Append("---");
        //        sb.Append(product.QtiToBuy.ToString());
        //        sb.AppendLine("---");
        //    }
        //    sb.AppendLine($"Business shelves required: {business.ShelvesNeeded}");
        //    sb.AppendLine();
        //}
        //sb.AppendLine($"Warehouse shelves required: {warehouse.ShelvesNeeded}");

        //if (!Directory.Exists(OutDirectory))
        //{
        //    Directory.CreateDirectory(OutDirectory);
        //}

        //await File.WriteAllTextAsync(Path.Combine(OutDirectory, $"report_{DateTime.Now:yyyyMMddHHmmss}.txt"), sb.ToString());

        var warehouse = new Warehouse();
        warehouse.Businesses = new List<Business>(await businessRepository.ListAsync());


        Path.Combine(Environment.CurrentDirectory, OutDirectory);

        await File.WriteAllTextAsync(Path.Combine(OutDirectory, $"report_{DateTime.Now:yyyyMMddHHmmss}.txt"),
            JsonSerializer.Serialize(warehouse, new JsonSerializerOptions { WriteIndented = true }));
    }
}
