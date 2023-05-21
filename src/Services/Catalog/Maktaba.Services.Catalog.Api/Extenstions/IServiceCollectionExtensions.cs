namespace Maktaba.Services.Catalog.Api;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
        });

        return services;
    }
}