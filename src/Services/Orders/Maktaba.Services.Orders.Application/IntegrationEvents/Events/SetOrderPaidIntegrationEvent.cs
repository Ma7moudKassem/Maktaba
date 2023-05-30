namespace Maktaba.Services.Orders.Application.IntegrationEvents.Events;

public class SetOrderPaidIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }
    public SetOrderPaidIntegrationEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}