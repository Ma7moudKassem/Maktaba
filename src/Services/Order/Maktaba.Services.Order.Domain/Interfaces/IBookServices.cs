namespace Maktaba.Services.Order.Domain;

public interface IBookServices
{
    Task<Book> GetBookByIdAsync(Guid id);
}