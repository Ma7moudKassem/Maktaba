using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
