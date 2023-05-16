namespace Maktaba.Services.Basket.Api;

public class BasketCheckout
{
    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public string CardHolderName { get; set; } = null!;

    public DateTime CardExpiration { get; set; }

    public string CardSecurityNumber { get; set; } = null!;

    public int CardTypeId { get; set; }

    public string Buyer { get; set; } = null!;

    public Guid RequestId { get; set; }
}