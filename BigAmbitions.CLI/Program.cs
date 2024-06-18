using BigAmbitions.Application.Extensions;
using BigAmbitions.CLI;
using BigAmbitions.Repository.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var serviceCollection = new ServiceCollection();
serviceCollection
    .RegisterServicesApplication()
    .RegisterServicesRepository()
    .AddSingleton<AppFacade>()
    .AddAutoMapper(Array.Empty<Assembly>());

var serviceProvider = serviceCollection.BuildServiceProvider();


var app = serviceProvider.GetRequiredService<AppFacade>();

await app.RunAsync();

public partial class Program { }