namespace Maktaba.Services.Basket.Api.IntegrationEvents.Services;

public class BasketIntegrationEventService : IBasketIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<BasketIntegrationEventService> _logger;
    public BasketIntegrationEventService(IEventBus eventBus,
        ILogger<BasketIntegrationEventService> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }
    public void PublishThroughEventBus(IntegrationEvent @event)
    {
        try
        {
            _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            _eventBus.Publish(@event);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        }
    }
}