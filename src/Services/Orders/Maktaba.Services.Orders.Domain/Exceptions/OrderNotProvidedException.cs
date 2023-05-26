namespace Maktaba.Services.Orders.Domain;

public sealed class OrderNotProvidedException : Exception
{
    public OrderNotProvidedException(Guid id) : base($"Order with id: {id} is not provided")
    { }
}
