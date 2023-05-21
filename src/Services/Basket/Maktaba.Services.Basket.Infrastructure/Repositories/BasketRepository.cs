namespace Maktaba.Services.Basket.Infrastructure;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabaseAsync _database;
    private readonly ConnectionMultiplexer _redis;

    public BasketRepository(IDatabaseAsync databse, ConnectionMultiplexer redis)
    {
        _database = databse;
        _redis = redis;
    }
    public async Task<bool> DeleteBasketAsync(string id) =>
       await _database.KeyDeleteAsync(id);

    public async Task<CustomerBasket?> GetBasketAsync(Guid userId)
    {
        var data = await _database.StringGetAsync(userId.ToString());

        if (data.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<CustomerBasket>(data, new JsonSerializerOptions
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

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        bool created = await _database.StringSetAsync(basket.UserId.ToString(), JsonSerializer.Serialize(basket));

        if (!created)
            return null;

        return await GetBasketAsync(basket.UserId);
    }

    IServer GetServer()
    {
        var endPoints = _redis.GetEndPoints();
        return _redis.GetServer(endPoints.First());
    }
}
