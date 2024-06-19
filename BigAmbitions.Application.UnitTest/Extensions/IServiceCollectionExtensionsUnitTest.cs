﻿using BigAmbitions.Application.Extensions;
using BigAmbitions.Repository.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace BigAmbitions.Application.UnitTest.Extensions;
public class IServiceCollectionExtensionsUnitTest
{
    const string ServiceCollectionNamespace = "BigAmbitions.Application.Contracts";
    private const string AssemblyName = "BigAmbitions.Application";
     
    [Fact]
    public void ShouldRegister_AllApplicationServices()
    {
        // Arrange
        var host = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((hostContext, config) =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true)
                            .AddEnvironmentVariables();
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.RegisterServicesRepository(hostContext.Configuration);
                        services.RegisterServicesApplication(); 
                    }).Build();


        // Act
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        // Assert
        Assembly assembly = Assembly.Load(AssemblyName);
        var interfaces = assembly.GetTypes()
            .Where(t => t.Namespace == ServiceCollectionNamespace && t.IsInterface);

        foreach (var currentInterface in interfaces)
        {
            var service = serviceProvider.GetService(currentInterface);
            service.Should().NotBeNull();
        }
    }
}
