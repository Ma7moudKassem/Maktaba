namespace Services.Shared;

public static class SharedExtensions
{
    public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultAuthentication(builder.Configuration);

        builder.Services.AddEventBus(builder.Configuration);

        builder.Services.AddHttpContextAccessor();

        builder.Host.AddSeriLog();

        return builder;
    }

    public static WebApplication UseServiceDefaults(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

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

    public static IServiceCollection AddEventBus(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

            ConnectionFactory factory = new()
            {
                HostName = configuration["RabbitMQEventBus:HostName"],
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(configuration["RabbitMQEventBus:UserName"]))
                factory.UserName = configuration["RabbitMQEventBus:UserName"];

            if (!string.IsNullOrEmpty(configuration["RabbitMQEventBus:Password"]))
                factory.Password = configuration["RabbitMQEventBus:Password"];

            int retryCount = configuration.GetValue("RabbitMQEventBus:RetryCount", 5);

            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
        {
            string? subscriptionClientName = configuration["RabbitMQEventBus:SubscriptionClientName"];
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
            var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            int retryCount = configuration.GetValue("RabbitMQEventBus:RetryCount", 5);

            return new EventBusRabbitMQ.EventBusRabbitMQ(
                rabbitMQPersistentConnection,
                logger,
                sp,
                eventBusSubcriptionsManager,
                subscriptionClientName,
                retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        return services;
    }

    public static IHostBuilder AddSeriLog(this IHostBuilder host)
    {
        var logger = new LoggerConfiguration()
            .WriteTo.Console()

            // Fatal
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
                .WriteTo.File($"Logs/{LogEventLevel.Fatal}-.log", rollingInterval: RollingInterval.Day))

            // Warning
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                .WriteTo.File($"Logs/{LogEventLevel.Warning}-.log", rollingInterval: RollingInterval.Day))

            // Error
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                .WriteTo.File($"Logs/{LogEventLevel.Error}-.log", rollingInterval: RollingInterval.Day))

            // Information
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                .WriteTo.File($"Logs/{LogEventLevel.Information}-.log", rollingInterval: RollingInterval.Day))

            // Debug
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
                .WriteTo.File($"Logs/{LogEventLevel.Debug}-.log", rollingInterval: RollingInterval.Day))

            // Verbose
            .WriteTo.Logger(l => l
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Verbose)
                .WriteTo.File($"Logs/{LogEventLevel.Verbose}-.log", rollingInterval: RollingInterval.Day));

        Log.Logger = logger.CreateLogger();

        host.UseSerilog();

        return host;
    }
}
