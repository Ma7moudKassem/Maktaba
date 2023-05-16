namespace Maktaba.Services.Order.Infrastructure;

public class UserServices : IUserServices
{
    private readonly HttpClient _httpClient;
    public UserServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Domain.User?> GetUserAsync(string userName) =>
        await _httpClient.GetFromJsonAsync<Domain.User>($"gateway/identity/users/?userName={userName}");
}