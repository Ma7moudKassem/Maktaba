namespace Maktaba.Services.Basket.Api.IntegrationEvents.Services;

public interface IBasketIntegrationEventService
{
    void PublishThroughEventBus(IntegrationEvent @event);
}