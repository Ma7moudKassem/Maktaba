namespace Maktaba.Services.Orders.Application.IntegrationEvents.Services;

public class OrderIntegrationEventService : IOrderIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<OrderIntegrationEventService> _logger;

    public OrderIntegrationEventService(IEventBus eventBus,
        ILogger<OrderIntegrationEventService> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public void PublishThroughEventBus(IntegrationEvent @event)
    {
        try
        {
            _logger.LogInformation("Publishing Integration Event: {EventId} - ({@IntegrationEvent})", @event.Id, @event);

            _eventBus.Publish(@event);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Publishing Integration Event: {EventId} - ({@IntegrationEvent})", @event.Id, @event);
            throw;
        }
    }
}
