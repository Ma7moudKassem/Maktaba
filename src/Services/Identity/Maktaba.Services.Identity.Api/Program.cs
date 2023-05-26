var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGrpc();
builder.Services.AddDomainLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseServiceDefaults();

using IServiceScope scope = app.Services.CreateScope();

IdentityDbContext context = scope.ServiceProvider
    .GetRequiredService<IdentityDbContext>();

context.Database.Migrate();

app.MapControllers();

app.MapGrpcService<UserRpcService>();

await app.RunAsync();