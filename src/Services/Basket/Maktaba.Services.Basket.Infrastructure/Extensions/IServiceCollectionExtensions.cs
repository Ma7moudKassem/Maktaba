namespace Maktaba.Services.Basket.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IBasketRepository, BasketRepository>()
                .AddTransient<IUserService, UserService>();

        services.AddRedis(configuration);

        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(sp =>
        {
            var config = ConfigurationOptions
            .Parse(configuration.GetConnectionString("RedisConnection") ?? "127.0.0.1", true);

            return ConnectionMultiplexer.Connect(config);
        });

        return services;
    }
}