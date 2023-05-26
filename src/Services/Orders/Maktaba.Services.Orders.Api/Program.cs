var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructureLayer();
builder.Services.AddApplicationLayer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseServiceDefaults();

using IServiceScope scop = app.Services.CreateScope();
OrderDbContext context = scop.ServiceProvider
    .GetRequiredService<OrderDbContext>();
context.Database.Migrate();

IEventBus eventBus = scop.ServiceProvider.GetRequiredService<IEventBus>();

eventBus.Subscribe<CheckoutCompletedEventIntegration, CheckoutCompletedEventIntegrationHandler>();

app.MapControllers();

await app.RunAsync();