namespace Maktaba.Services.Orders.Domain;

public sealed class BookNotProvidedException : Exception
{
    public BookNotProvidedException(Guid bookId) :
        base($"Book with id: {bookId} is not provided")
    {

    }
}