namespace Maktaba.Services.Orders.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string CardNumber { get; set; } = null!;
    public string SecurityNumber { get; set; } = null!;
    public string CardHolderName { get; set; } = null!;
    public CardType CardType { get; set; }
    public DataType ExpirationDate { get; set; }

    public PaymentMethod(string cardNumber,
        string securityNumber,
        string cardHolderName,
        CardType cardType,
        DataType expirationDate)
    {
        CardNumber = cardNumber;
        SecurityNumber = securityNumber;
        CardHolderName = cardHolderName;
        CardType = cardType;
        ExpirationDate = expirationDate;
    }
}