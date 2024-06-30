namespace BigAmbitions.WebApi.Extensions;

public static class IServiceCollectionsExtensions
{
    public static IServiceCollection RegisterServicesWebApi(this IServiceCollection serviceCollections)
     => serviceCollections.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}
