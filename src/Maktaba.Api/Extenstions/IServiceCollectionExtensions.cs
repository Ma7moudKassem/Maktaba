using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Maktaba.Api;

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

    public static IServiceCollection ConfigureIISIntegration(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options => { });

        return services;
    }
}