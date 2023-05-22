namespace Maktaba.Services.Order.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDataBase();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7037")
        }).AddScoped<IUserServices, UserServices>()
          .AddScoped<IBookServices, BookServices>();

        return services;
    }

    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        services.AddDbContext<OrderDbContext>(e =>
        e.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        using IServiceScope serviceScope = services.BuildServiceProvider()
            .GetRequiredService<IServiceScopeFactory>().CreateScope();

        OrderDbContext? context = serviceScope.ServiceProvider
            .GetService<OrderDbContext>();

        context?.Database.Migrate();

        return services;
    }
}
