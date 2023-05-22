var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddSingleton(sp =>
{
    string connection = builder.Configuration.GetConnectionString("RedisConnection") ??
    throw new ArgumentNullException("RedisConnection");
    var config = ConfigurationOptions.Parse(connection, true);
    return ConnectionMultiplexer.Connect(config);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseServiceDefaults();

app.MapControllers();

await app.RunAsync();