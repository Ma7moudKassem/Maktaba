namespace Maktaba.Services.Catalog.Application;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(e =>
        e.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddIntegrationServices(configuration)
                .AddEventBus(configuration);

        return services;
    }

    public static IServiceCollection AddIntegrationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

            ConnectionFactory factory = new()
            {
                HostName = configuration["RabbitMQEventBus:HostName"],
            };

            if (!string.IsNullOrEmpty(configuration["RabbitMQEventBus:UserName"]))
                factory.UserName = configuration["RabbitMQEventBus:UserName"];

            if (!string.IsNullOrEmpty(configuration["RabbitMQEventBus:Password"]))
                factory.Password = configuration["RabbitMQEventBus:Password"];

            int retryCount = 5;
            if (!string.IsNullOrEmpty(configuration["RabbitMQEventBus:RetryCount"]))
                retryCount = int.Parse(configuration["RabbitMQEventBus:RetryCount"] ?? "5");

            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
        {
            string? subscriptionClientName = configuration["RabbitMQEventBus:SubscriptionClientName"];
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

            int retryCount = 5;
            if (!string.IsNullOrEmpty(configuration["RabbitMQEventBus:RetryCount"]))
            {
                retryCount = int.Parse(configuration["RabbitMQEventBus:RetryCount"] ?? "5");
            }

            return new EventBusRabbitMQ.EventBusRabbitMQ(
                rabbitMQPersistentConnection,
                logger,
                sp,
                eventBusSubcriptionsManager,
                subscriptionClientName,
                retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        return services;
    }
}
