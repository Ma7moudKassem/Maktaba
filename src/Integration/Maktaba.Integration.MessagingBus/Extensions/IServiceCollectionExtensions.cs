namespace Maktaba.Integration.MessagingBus;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqRequiredServices(
        this IServiceCollection services)
    {
        services.AddMassTransit(mass =>
        {
            mass.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddScoped<IMessageBus, RabbitMQMessageBus>();

        return services;
    }
}
