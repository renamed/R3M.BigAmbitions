using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BigAmbitions.Repository.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterServicesRepository(this IServiceCollection services)
        => services
            .AddSingleton<IBusinessRepository, BusinessRepository>()
            .AddSingleton<IProductConfigRepository, ProductConfigRepository>()
            .AddSingleton<IProductRepository, ProductRepository>()
            .AddSingleton<IReportRepository, ReportRepository>()
            .AddDbContext<BigAmbitionContext>(opt => opt.UseSqlServer())
            .AddAutoMapper(Array.Empty<Assembly>());
        
}
