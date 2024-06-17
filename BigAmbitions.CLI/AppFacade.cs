using BigAmbitions.Domain.Config;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using BigAmbitions.Application.Contracts;
using BigAmbitions.Application.Extensions;

namespace BigAmbitions.CLI;
public class AppFacade
{
    private readonly IProductConfigRepository productConfigRepository;
    private readonly IReportApplication reportApplication;
    private readonly IBusinessRepository businessRepository;

    public AppFacade(IProductConfigRepository productConfigRepository, IReportApplication reportApplication, IBusinessRepository businessRepository)
    {
        this.productConfigRepository = productConfigRepository;
        this.reportApplication = reportApplication;
        this.businessRepository = businessRepository;
    }

    public async ValueTask RunAsync()
    {
        var productsConfig = (await productConfigRepository.ListAsync().ToListAsync()).ToDictionary(k => k.Id, v => v);

        string productsMenu = GetProductsMenu(productsConfig);


        var businessName = ReadString("business name");
        while (!string.IsNullOrWhiteSpace(businessName))
        {
            var business = new Business
            {
                Name = businessName,
                DaysOpened = ReadInt("days open")
            };

            var productConfig = GetProductName(productsConfig, productsMenu);
            while (productConfig is not null)
            {

                var product = new Product
                {
                    Name = productConfig.Name,
                    SoldLast7Days = ReadInt("sold last 7 days")
                };

                product.SetQtiToBuy(business.DaysOpened, productConfig.QtiPerBox);
                product.SetBusinessBoxesNeeded(productConfig.QtiPerBox);

                business.Products.Add(product);

                productConfig = GetProductName(productsConfig, productsMenu);
            }
            
            await businessRepository.AddAsync(business);
            businessName = ReadString("business name");
        }

        await businessRepository.SaveAsync();
        await reportApplication.GenerateReportAsync();
    }

    private static string ReadString(string input, string messageTemplate = "Enter {0}: ")
    {
        var message = messageTemplate == null ? input : string.Format(messageTemplate, input);

        Console.Write(message);
        return Console.ReadLine();
    }

    private static int ReadInt(string input, string messageTemplate = "Enter {0}: ")
    {
        return int.TryParse(ReadString(input, messageTemplate), out var result) ? result : 0;
    }

    private static ProductConfig GetProductName(IReadOnlyDictionary<int, ProductConfig> productsConfig, string productsMenu)
    {
        var productId = ReadInt(productsMenu, null);
        productsConfig.TryGetValue(productId, out var product);
        return product;
    }


    private static string FormatProductColumn(IEnumerable<KeyValuePair<int, ProductConfig>> products)
    {
        return string.Join(Environment.NewLine, products.Select(x => $"{x.Key} - {x.Value.Name}"));
    }

    private static string GetProductsMenu(Dictionary<int, ProductConfig> productsConfig)
    {
        var halfwayIndex = productsConfig.Count / 2;

        IEnumerable<KeyValuePair<int, ProductConfig>> firstHalf = productsConfig.Take(halfwayIndex);
        var secondHalf = productsConfig.Skip(halfwayIndex);

        var firstColumn = FormatProductColumn(firstHalf);
        var secondColumn = FormatProductColumn(secondHalf);

        var combinedColumns = CombineColumns(firstColumn, secondColumn);

        return combinedColumns;
    }
    private static string CombineColumns(string column1, string column2)
    {
        var lines1 = column1.Split(Environment.NewLine);
        var lines2 = column2.Split(Environment.NewLine);

        var maxLength = Math.Max(lines1.Length, lines2.Length);
        var result = new List<string>();

        for (int i = 0; i < maxLength; i++)
        {
            var line1 = i < lines1.Length ? lines1[i] : "";
            var line2 = i < lines2.Length ? lines2[i] : "";

            result.Add($"{line1,-40}{line2}");
        }

        return string.Join(Environment.NewLine, result);
    }
}
