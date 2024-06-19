using BigAmbitions.Application.Contracts;
using BigAmbitions.Application.Validators;
using BigAmbitions.Domain;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BigAmbitions.Application.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterServicesApplication(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<IWarehouseApplication, WarehouseApplication>()
            .AddScoped<IValidator<Warehouse>, WarehouseValidator>();
}
