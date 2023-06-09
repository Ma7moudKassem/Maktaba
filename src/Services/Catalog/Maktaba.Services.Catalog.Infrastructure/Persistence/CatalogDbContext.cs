﻿namespace Maktaba.Services.Catalog.Infrastructure;

public class CatalogDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public CatalogDbContext(DbContextOptions options, IConfiguration configuration) : base(options) =>
        _configuration = configuration;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlConnection"),
                b => b.MigrationsAssembly("Maktaba.Services.Catalog.Infrastructure"));

        base.OnConfiguring(optionsBuilder);
    }
}