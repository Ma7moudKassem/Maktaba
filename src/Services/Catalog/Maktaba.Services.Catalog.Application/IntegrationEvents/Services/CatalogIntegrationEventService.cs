namespace Maktaba.Services.Catalog.Application;

public class CatalogIntegrationEventService : ICatalogIntegrationEventService
{
    private readonly IEventBus _eventBus;
    public CatalogIntegrationEventService(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void PublisThroughEventBusAsync(IntegrationEvent @event)
    {
        try
        {
            _eventBus.Publish(@event);
        }
        catch (Exception ex)
        {
            Log.Error(ex.GetExceptionErrorSimplified());
            throw;
        }
    }
}