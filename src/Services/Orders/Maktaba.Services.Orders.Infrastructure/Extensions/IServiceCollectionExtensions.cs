namespace Maktaba.Services.Orders.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDbContext<OrderDbContext>(e =>
        e.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        //services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7037")
        });

        return services;
    }
}
