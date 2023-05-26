using Microsoft.EntityFrameworkCore.Storage;

namespace Maktaba.Services.Orders.Infrastructure;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }

    private readonly IConfiguration _configuration;
    public OrderDbContext(DbContextOptions options, IConfiguration configuration) : base(options) =>
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
                b => b.MigrationsAssembly("Maktaba.Services.Orders.Infrastructure"));

        base.OnConfiguring(optionsBuilder);
    }

    public async Task<IDbContextTransaction?> BeginTransactionAsync() =>
        Database.CurrentTransaction ?? await Database.BeginTransactionAsync();
}
