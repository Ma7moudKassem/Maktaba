namespace Maktaba.Services.Order.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMassTransitConsumer
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            //x.AddConsumer<UserConsumer>();
            //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            //{
            //    cfg.Host(
            //        new Uri(configuration["RabbitMQ:HostAddress"]
            //        ?? throw new AppSettingsJsonException("RabbitMQ:HostAddress")), h =>
            //        {
            //            h.Username(configuration["RabbitMQ:UserName"]
            //        ?? throw new AppSettingsJsonException("RabbitMQ:UserName"));

            //            h.Password(configuration["RabbitMQ:Password"]
            //        ?? throw new AppSettingsJsonException("RabbitMQ:Password"));

            //        });

            //    cfg.ReceiveEndpoint("user-queue", ep =>
            //    {
            //        ep.ConfigureConsumer<UserConsumer>(provider);
            //    });

            //    cfg.ReceiveEndpoint("book-queue", ep =>
            //    {
            //        ep.ConfigureConsumer<BookCreatedConsumer>(provider);
            //    });
            //}));
        });

        return services;
    }
}
