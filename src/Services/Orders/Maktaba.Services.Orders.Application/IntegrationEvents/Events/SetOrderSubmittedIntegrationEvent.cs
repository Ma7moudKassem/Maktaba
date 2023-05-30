namespace Maktaba.Services.Orders.Application.IntegrationEvents.Events;

public class SetOrderSubmittedIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }

    public SetOrderSubmittedIntegrationEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}
