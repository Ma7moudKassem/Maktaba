using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Services.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("JWT");

        if (!jwtSection.Exists())
            return services;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"] ??
                throw new ArgumentNullException("JWT:Key")))
            };
        });

        return services;
    }
}
