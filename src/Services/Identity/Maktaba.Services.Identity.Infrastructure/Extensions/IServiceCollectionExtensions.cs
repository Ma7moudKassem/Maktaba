namespace Maktaba.Services.Identity.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.ConfigureJwt(configuration);

        return services;
    }

    public static IServiceCollection ConfigureJwt(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JWT>(configuration.GetSection("JWT"));

        services.AddScoped<IAuthService, AuthService>();

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>();

        return services;
    }
}