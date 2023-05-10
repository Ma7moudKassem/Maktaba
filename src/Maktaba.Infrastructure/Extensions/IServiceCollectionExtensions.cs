namespace Maktaba.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataBase();
        services.ConfigureJwtToken(configuration);

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
        services.AddDbContext<MaktabaDbContext>(e =>
        e.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        using IServiceScope serviceScope = services.BuildServiceProvider()
            .GetRequiredService<IServiceScopeFactory>().CreateScope();

        MaktabaDbContext? context = serviceScope.ServiceProvider
            .GetService<MaktabaDbContext>();

        context?.Database.Migrate();

        return services;
    }

    public static IServiceCollection ConfigureJwtToken(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtTokenValidationService, JwtTokenValidationService>();
        services.AddAuthentication()
          .AddJwtBearer(cfg =>
          {
              cfg.TokenValidationParameters = new MaktabaTokenValidationParameters(configuration);
          });

        services.AddDefaultIdentity<IdentityUser>()
          .AddEntityFrameworkStores<MaktabaDbContext>();

        return services;
    }
}