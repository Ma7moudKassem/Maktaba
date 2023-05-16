namespace Maktaba.Services.Catalog.Domain;

public sealed class BookNotProvidedException : Exception
{
    public BookNotProvidedException(string id) :
        base($"Book with id: {id} is not provided")
    {

    }
}