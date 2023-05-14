namespace Maktaba.Integration.MessagingBus;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqRequiredServices(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            }));
        });
        services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(30);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });
        services.AddScoped<IMessageBus, RabbitMQMessageBus>();

        return services;
    }
}
