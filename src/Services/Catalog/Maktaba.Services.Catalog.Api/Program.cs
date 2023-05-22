var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGrpc();
builder.Services.AddDomainLayer();
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseServiceDefaults();

app.MapControllers();

app.MapGrpcService<CatalogService>();

await app.RunAsync();