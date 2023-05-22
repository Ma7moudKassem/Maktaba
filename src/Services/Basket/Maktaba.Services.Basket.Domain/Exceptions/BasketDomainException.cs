namespace Maktaba.Services.Basket.Domain.Exceptions;

public sealed class BasketDomainException : Exception
{
    public BasketDomainException(string message) : base(message)
    { }
}