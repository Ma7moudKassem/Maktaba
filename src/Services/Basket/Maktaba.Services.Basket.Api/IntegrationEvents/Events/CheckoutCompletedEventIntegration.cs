namespace Maktaba.Services.Basket.Api.IntegrationEvents.Events;

public class CheckoutCompletedEventIntegration : IntegrationEvent
{
    public UserBasket Basket { get; }
    public string UserIdentity { get; set; }
    public string UserName { get; set; }
    public string Buyer { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public string CardHolderName { get; set; } = null!;
    public DateTime CardExpiration { get; set; }
    public string CardSecurityNumber { get; set; } = null!;
    public int CardTypeId { get; set; }

    public CheckoutCompletedEventIntegration(
        string userIdentity,
        string userName,
        string city,
        string street,
        string country,
        string cardNumber,
        string cardHolderName,
        DateTime cardExpiration,
        string cardSecurityNumber,
        int cardTypeId,
        string buyer,
        UserBasket basket)
    {
        UserIdentity = userIdentity;
        UserName = userName;
        City = city;
        Street = street;
        Country = country;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        CardExpiration = cardExpiration;
        CardSecurityNumber = cardSecurityNumber;
        CardTypeId = cardTypeId;
        Buyer = buyer;
        Basket = basket;
    }
}