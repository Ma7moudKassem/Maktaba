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

        CustomerBasket? basket = await _repository.GetBasketAsync(new Guid(request.Id));

        if (basket is not null)
            return MapToCustomerBasketResponse(basket);

        return new CustomerBasketResponse();
    }

    public async override Task<CustomerBasketResponse> UpdateBasket(CustomerBasketRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Call gRPC to update user basket that has id: {Id}", request.UserId);

        var customerBasket = MapToCustomerBasket(request);

        var response = await _repository.UpdateBasketAsync(customerBasket);

        if (response is not null)
            return MapToCustomerBasketResponse(response);

        return new CustomerBasketResponse();
    }

    static CustomerBasket MapToCustomerBasket(CustomerBasketRequest request)
    {
        var basket = new CustomerBasket
        {
            UserId = new Guid(request.UserId),
        };

        request.Items.ToList().ForEach(x =>
        {
            basket.Items.Add(new BasketItem
            {
                Id = new Guid(x.Id),
                BookId = new Guid(x.BookId),
                BookName = x.BookTitle,
                UnitPrice = (decimal)x.Price,
                OldUnitPrice = (decimal)x.OldPrice,
                PictureUrl = x.Pictureurl,
                Quantity = x.Quantity,
            });
        });

        return basket;
    }

    static CustomerBasketResponse MapToCustomerBasketResponse(CustomerBasket basket)
    {
        CustomerBasketResponse response = new()
        {
            UserId = basket.UserId.ToString(),
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