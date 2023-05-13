namespace Maktaba.Services.Order.Infrastructure;

public class BookServices : IBookServices
{
    private readonly HttpClient _httpClient;
    public BookServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Book> GetBookByIdAsync(Guid id) =>
        await _httpClient.GetFromJsonAsync<Book>($"gateway/catalog/books/{id}") ??
            throw new BookNotProvidedException(id);
}