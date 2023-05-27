namespace Maktaba.Services.Orders.Application.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(LoggingBehaviors<,>));
        });

        services.AddTransient<IOrderIntegrationEventService, OrderIntegrationEventService>();
        services.AddTransient<IIntegrationEventHandler<CheckoutCompletedEventIntegration>, CheckoutCompletedEventIntegrationHandler>();

        return services;
    }
}