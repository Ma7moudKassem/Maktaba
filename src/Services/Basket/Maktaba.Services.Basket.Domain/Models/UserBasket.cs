namespace Maktaba.Services.Basket.Domain;

public class UserBasket
{
    public UserBasket(string userIdentity)
    {
        UserIdentity = userIdentity;
    }

    public UserBasket() { }

    public string UserIdentity { get; set; } = null!;
    public List<BasketItem> Items { get; set; } = new();
}