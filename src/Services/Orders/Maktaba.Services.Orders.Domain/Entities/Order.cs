namespace Maktaba.Services.Orders.Domain.Entities;

public class Order : BaseEntity
{
    public Guid AddressId { get; set; }
    public Address Address { get; set; } = new();
    public Guid UserId { get; set; }
    public User User { get; set; } = new();
    public OrderStatus OrderStatus { get; set; }

    public Order(Guid addressId,
        Guid userId,
        OrderStatus orderStatus)
    {
        AddressId = addressId;
        UserId = userId;
        OrderStatus = orderStatus;
    }

    public Order() { }

    public void CancelOrder() => this.OrderStatus = OrderStatus.Canceled;
    public void SetOrderPaid() => this.OrderStatus = OrderStatus.Paid;
    public void SetOrderShipped() => this.OrderStatus = OrderStatus.Shipped;
    public void SetOrderSubmitted() => this.OrderStatus = OrderStatus.Submitted;
}