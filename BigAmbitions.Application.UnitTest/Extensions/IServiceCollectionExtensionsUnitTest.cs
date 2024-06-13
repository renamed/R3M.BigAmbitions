using BigAmbitions.Application.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
        IServiceCollection serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.RegisterServicesApplication();
        var serviceProvider = serviceCollection.BuildServiceProvider();

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
