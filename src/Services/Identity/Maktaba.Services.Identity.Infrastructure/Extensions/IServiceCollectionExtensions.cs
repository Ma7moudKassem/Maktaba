namespace Maktaba.Services.Identity.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDatabase();

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<IdentityDbContext>();

        using IServiceScope scope = services.BuildServiceProvider().CreateScope();

        IdentityDbContext? context =
            scope.ServiceProvider.GetService<IdentityDbContext>();

        context?.Database.Migrate();

        return services;
    }
}
