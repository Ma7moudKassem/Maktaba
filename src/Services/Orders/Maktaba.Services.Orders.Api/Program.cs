var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructureLayer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseServiceDefaults();

using IServiceScope scop = app.Services.CreateScope();
OrderDbContext context = scop.ServiceProvider
    .GetRequiredService<OrderDbContext>();
context.Database.Migrate();

app.MapControllers();

await app.RunAsync();