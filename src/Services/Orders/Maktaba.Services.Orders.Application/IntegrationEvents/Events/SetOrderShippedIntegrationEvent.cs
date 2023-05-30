namespace Maktaba.Services.Orders.Application.IntegrationEvents.Events;

public class SetOrderShippedIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }

    public SetOrderShippedIntegrationEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}
