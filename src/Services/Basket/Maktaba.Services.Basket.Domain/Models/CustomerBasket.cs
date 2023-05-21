namespace Maktaba.Services.Basket.Domain;

public class CustomerBasket
{
    public CustomerBasket(Guid userId)
    {
        UserId = userId;
    }

    public CustomerBasket()
    {

    }

    public Guid UserId { get; set; }
    public List<BasketItem> Items { get; set; } = new();
}