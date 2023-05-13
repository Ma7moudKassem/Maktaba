namespace Maktaba.Services.Order.Infrastructure;

public class UserServices : IUserServices
{
    private readonly HttpClient _httpClient;
    public UserServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User?> GetUserAsync(string userName) =>
        await _httpClient.GetFromJsonAsync<User>($"gateway/catalog/users/?userName={userName}");
}