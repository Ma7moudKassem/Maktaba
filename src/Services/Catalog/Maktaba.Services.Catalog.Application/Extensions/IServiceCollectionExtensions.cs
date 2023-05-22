namespace Maktaba.Services.Catalog.Application;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(e =>
        e.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>()
                .AddIntegrationServices(configuration);

        services.AddEventBus(configuration);

        return services;
    }
}