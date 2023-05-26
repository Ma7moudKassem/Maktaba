namespace Maktaba.Services.Orders.Application.IntegrationEvents.Services;

public interface IOrderIntegrationEventService
{
    void PublishThroughEventBus(IntegrationEvent @event);
}
