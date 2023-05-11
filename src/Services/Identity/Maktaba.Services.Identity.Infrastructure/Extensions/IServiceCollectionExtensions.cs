namespace Maktaba.Services.Identity.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase();

        services.ConfigureJwt(configuration);

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

    public static IServiceCollection ConfigureJwt(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JWT>(configuration.GetSection("JWT"));

        services.AddScoped<IAuthService, AuthService>();

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? string.Empty)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}