namespace Maktaba.Services.Orders.Application.IntegrationEvents.Events;

public class OrderHasCancelledIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Country { get; set; } = null!;

    public OrderHasCancelledIntegrationEvent(Guid orderId,
        Guid userId,
        string userName,
        string city,
        string street,
        string country)
    {
        OrderId = orderId;
        UserId = userId;
        UserName = userName;
        City = city;
        Street = street;
        Country = country;
    }
}