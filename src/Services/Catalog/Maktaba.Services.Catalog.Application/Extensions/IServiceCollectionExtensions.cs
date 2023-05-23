namespace Maktaba.Services.Catalog.Application;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(e =>
        e.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

        return services;
    }
}