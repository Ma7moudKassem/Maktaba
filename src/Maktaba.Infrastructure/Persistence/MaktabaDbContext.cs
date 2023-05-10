﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Maktaba.Infrastructure;

public class MaktabaDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public MaktabaDbContext(DbContextOptions options, IConfiguration configuration) : base(options) =>
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
                b => b.MigrationsAssembly("Maktaba.Infrastructure"));

        base.OnConfiguring(optionsBuilder);
    }
}