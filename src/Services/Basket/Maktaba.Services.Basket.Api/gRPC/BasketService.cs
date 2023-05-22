namespace Maktaba.Services.Basket;

public class BasketService : BasketBase
{
    private readonly IBasketRepository _repository;
    private readonly ILogger<BasketService> _logger;
    public BasketService(IBasketRepository repository, ILogger<BasketService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public override async Task<CustomerBasketResponse> GetBasketById(BasketRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Call gRPC to get basket with id: {Id}", request.Id);

        UserBasket? basket = await _repository.GetBasketAsync(request.Id);

        if (basket is not null)
            return MapToCustomerBasketResponse(basket);

        return new CustomerBasketResponse();
    }

    public async override Task<CustomerBasketResponse> AddBasket(CustomerBasketRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Call gRPC to update user basket that has id: {Id}", request.UserId);

        var customerBasket = MapToCustomerBasket(request);

        var response = await _repository.AddBasketAsync(customerBasket);

        if (response is not null)
            return MapToCustomerBasketResponse(response);

        return new CustomerBasketResponse();
    }

    static UserBasket MapToCustomerBasket(CustomerBasketRequest request)
    {
        var basket = new UserBasket
        {
            UserIdentity = request.UserId,
        };

        request.Items.ToList().ForEach(x =>
        {
            basket.Items.Add(new BasketItem
            {
                Id = Guid.Parse(x.Id),
                BookId = Guid.Parse(x.BookId),
                BookName = x.BookTitle,
                UnitPrice = (decimal)x.Price,
                OldUnitPrice = (decimal)x.OldPrice,
                PictureUrl = x.Pictureurl,
                Quantity = x.Quantity,
            });
        });

        return basket;
    }

    static CustomerBasketResponse MapToCustomerBasketResponse(UserBasket basket)
    {
        CustomerBasketResponse response = new()
        {
            UserId = basket.UserIdentity,
        };

        basket.Items.ForEach(x =>
        {
            response.Items.Add(new BasketItemResponce
            {
                Id = x.Id.ToString(),
                BookId = x.BookId.ToString(),
                BookTitle = x.BookName,
                Price = (double)x.UnitPrice,
                OldPrice = (double)x.OldUnitPrice,
                Pictureurl = x.PictureUrl,
                Quantity = x.Quantity
            });
        });

        return response;
    }
}