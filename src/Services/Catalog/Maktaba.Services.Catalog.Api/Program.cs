var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGrpc();
builder.Services.AddDomainLayer();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseServiceDefaults();

using IServiceScope scope = app.Services.CreateScope();

CatalogDbContext context = scope.ServiceProvider
    .GetRequiredService<CatalogDbContext>();

await context.Database.MigrateAsync();

app.MapControllers();

app.MapGrpcService<CatalogService>();

await app.RunAsync();