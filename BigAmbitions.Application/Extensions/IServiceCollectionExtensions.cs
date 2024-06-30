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
            // Validators
            .AddSingleton<IValidator<Employee>, EmployeeValidator>()
            .AddSingleton<IValidator<Business>, BusinessValidator>()
            .AddSingleton<IValidator<Game>, GameValidator>()
            // Applications
            .AddScoped<IEmployeeApplication, EmployeeApplication>()
            .AddScoped<IBusinessesApplication, BusinessesApplication>()
            .AddScoped<IGameApplication, GameApplication>();
}
