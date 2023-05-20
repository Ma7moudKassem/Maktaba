namespace Maktaba.Services.Catalog.Application;

public class CatalogIntegrationEventService : ICatalogIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<CatalogIntegrationEventService> _logger;
    public CatalogIntegrationEventService(IEventBus eventBus,
        ILogger<CatalogIntegrationEventService> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public void PublisThroughEventBusAsync(IntegrationEvent @event)
    {
        try
        {
            _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} from Catalog.Application - ({@IntegrationEvent})", @event.Id, @event);

            _eventBus.Publish(@event);
        }
        catch (Exception ex)
        {
            Log.Error(ex.GetExceptionErrorSimplified());
            throw;
        }
    }
}