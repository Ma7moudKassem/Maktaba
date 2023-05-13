namespace Maktaba.Services.Order.Domain;

public sealed class OrderNotProvidedException : Exception
{
    public OrderNotProvidedException() : base("This order is not provided")
    {

    }
}
