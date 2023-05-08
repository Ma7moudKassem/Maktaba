﻿namespace Maktaba.Application;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(e =>
        e.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }
}
