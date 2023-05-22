namespace Maktaba.Services.Catalog.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDataBase();

        services.AddScoped<IBookRepository, BookRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }

    public static IServiceCollection AddDataBase(this IServiceCollection services)
    {
        services.AddDbContext<CatalogDbContext>(e =>
        e.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        using IServiceScope serviceScope = services.BuildServiceProvider()
            .GetRequiredService<IServiceScopeFactory>().CreateScope();

        CatalogDbContext? context = serviceScope.ServiceProvider
            .GetService<CatalogDbContext>();

        //context?.Database.Migrate();

        return services;
    }
}