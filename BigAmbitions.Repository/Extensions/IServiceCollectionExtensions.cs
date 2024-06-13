using BigAmbitions.Repository.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BigAmbitions.Repository.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterServicesRepository(this IServiceCollection services)
        => services
            .AddSingleton<IBusinessRepository, BusinessRepository>()
            .AddSingleton<IProductConfigRepository, ProductConfigRepository>()
            .AddSingleton<IProductRepository, ProductRepository>()
            .AddSingleton<IReportRepository, ReportRepository>();
}
