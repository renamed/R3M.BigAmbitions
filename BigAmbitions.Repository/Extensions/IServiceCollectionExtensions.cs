using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BigAmbitions.Repository.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterServicesRepository(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddScoped<IBusinessRepository, BusinessRepository>()
            .AddScoped<IProductRepository, ProductRepository>()            
            .AddScoped<IBigAmbitionContext, BigAmbitionContext>()
            .AddDbContext<BigAmbitionContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("BigAmbitions")))
            .AddAutoMapper(Array.Empty<Assembly>());
        
}
