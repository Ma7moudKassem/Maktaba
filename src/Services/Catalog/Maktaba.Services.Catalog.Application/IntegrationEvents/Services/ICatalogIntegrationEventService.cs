namespace Maktaba.Services.Catalog.Application;

public interface ICatalogIntegrationEventService
{
    void PublisThroughEventBusAsync(IntegrationEvent @event);
}