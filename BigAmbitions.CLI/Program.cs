using BigAmbitions.Application.Extensions;
using BigAmbitions.CLI;
using BigAmbitions.Repository.Extensions;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection
    .RegisterServicesApplication()
    .RegisterServicesRepository()
    .AddSingleton<AppFacade>();

var serviceProvider = serviceCollection.BuildServiceProvider();


var app = serviceProvider.GetRequiredService<AppFacade>();

await app.RunAsync();