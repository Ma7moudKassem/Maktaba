using EventBus.Extensions;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Publishing event: {eventName}", @event.GetGenericTypeName());

            _eventBus.Publish(@event);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "Faild to publish event: {eventName} , exception: {exceptionName}",
                @event.GetGenericTypeName(),
                ex.GetExceptionErrorSimplified());

            throw;
        }
    }
}