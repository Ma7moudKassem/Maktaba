namespace Maktaba.Services.Catalog.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataBase();

        services.AddScoped<IBookRepository, BookRepository>()
                .AddScoped<ILibraryRepository, LibraryRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Hour)
            .CreateLogger();

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

        context?.Database.Migrate();

        return services;
    }
}