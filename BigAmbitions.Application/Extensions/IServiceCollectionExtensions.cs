using BigAmbitions.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BigAmbitions.Application.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterServicesApplication(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IReportApplication, ReportApplication>();
}
