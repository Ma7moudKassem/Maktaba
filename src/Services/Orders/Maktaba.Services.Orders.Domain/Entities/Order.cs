namespace Maktaba.Services.Orders.Domain.Entities;

public class Order : BaseEntity
{
    public Guid AddressId { get; set; }
    public Address? Address { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public Order(Guid addressId,
        Guid userId,
        OrderStatus orderStatus)
    {
        AddressId = addressId;
        UserId = userId;
        OrderStatus = orderStatus;
    }
}