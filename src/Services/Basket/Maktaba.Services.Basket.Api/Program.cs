var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddTransient<IBasketIntegrationEventService, BasketIntegrationEventService>();
builder.Services.AddTransient<IIntegrationEventHandler<BookPriceChangedIntegrationEvent>, BookPriceChangedIntegrationEventHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseServiceDefaults();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<BookPriceChangedIntegrationEvent, BookPriceChangedIntegrationEventHandler>();

await app.RunAsync();