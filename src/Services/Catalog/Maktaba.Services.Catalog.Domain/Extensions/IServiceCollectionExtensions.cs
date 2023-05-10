namespace Maktaba.Services.Catalog.Domain;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDomainLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}