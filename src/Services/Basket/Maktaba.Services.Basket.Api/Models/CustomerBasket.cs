namespace Maktaba.Services.Basket.Api;

public class CustomerBasket
{
    public CustomerBasket(Guid customerId)
    {
        BuyerId = customerId;
    }

    public Guid BuyerId { get; set; }
    public List<BasketItem> Items { get; set; } = new();
}
