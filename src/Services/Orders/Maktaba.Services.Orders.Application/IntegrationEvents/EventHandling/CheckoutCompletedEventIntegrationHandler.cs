namespace Maktaba.Services.Orders.Application.IntegrationEvents.EventHandling;

public class CheckoutCompletedEventIntegrationHandler : IIntegrationEventHandler<CheckoutCompletedEventIntegration>
{
    public Task Handle(CheckoutCompletedEventIntegration @event)
    {
        throw new NotImplementedException();
    }
}