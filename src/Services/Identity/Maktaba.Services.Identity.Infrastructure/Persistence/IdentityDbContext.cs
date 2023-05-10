namespace Maktaba.Services.Identity.Infrastructure;

public class IdentityDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _configuration;
    public IdentityDbContext(DbContextOptions options, IConfiguration configuration) :
        base(options) => _configuration = configuration;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlConnection"),
                e => e.MigrationsAssembly("Maktaba.Services.Identity.Infrastructure"));
        }

        base.OnConfiguring(optionsBuilder);
    }
}