using BigAmbitions.Repository.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BigAmbitions.Application.UnitTest.Extensions;
public class IServiceCollectionExtensionsUnitTest
{
    const string ServiceCollectionNamespace = "BigAmbitions.Repository.Contracts";
    private const string AssemblyName = "BigAmbitions.Repository";

    [Fact]
    public void ShouldRegister_AllRepositoryServices()
    {
        // Arrange
        IServiceCollection serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.RegisterServicesRepository();
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
