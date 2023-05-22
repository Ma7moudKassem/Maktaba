namespace Maktaba.Services.Basket.Infrastructure;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;
    private readonly ConnectionMultiplexer _redis;

    public BasketRepository(ConnectionMultiplexer redis)
    {
        _redis = redis;
        _database = redis.GetDatabase();
    }
    public async Task<bool> DeleteBasketAsync(string userIdentity) =>
       await _database.KeyDeleteAsync(userIdentity);

    public async Task<UserBasket?> GetBasketAsync(string userId)
    {
        var data = await _database.StringGetAsync(userId.ToString());

        if (data.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<UserBasket?>(data, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }

    public IEnumerable<string> GetUsers()
    {
        IServer server = GetServer();
        var data = server.Keys();

        return data.Select(key => key.ToString());
    }

    public async Task<UserBasket?> AddBasketAsync(UserBasket basket)
    {
        bool created = await _database.StringSetAsync(basket.UserIdentity,
            JsonSerializer.Serialize(basket));

        if (!created)
            return null;

        return await GetBasketAsync(basket.UserIdentity);
    }

    IServer GetServer()
    {
        var endPoints = _redis.GetEndPoints();
        return _redis.GetServer(endPoints.First());
    }
}