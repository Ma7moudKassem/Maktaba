namespace Maktaba.Services.Catalog.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDbContext<CatalogDbContext>(e =>
        e.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddScoped<IBookRepository, BookRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}