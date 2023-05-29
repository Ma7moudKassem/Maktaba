namespace Maktaba.Services.Orders.Application.Handlers.QueriesHandlers;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order?>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<GetOrderQueryHandler> _logger;

    public GetOrderQueryHandler(IOrderRepository repository,
        ILogger<GetOrderQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Order with id: {OrderId}", request.Id);

        return await _repository.GetOrderAsync(request.Id,cancellationToken);
    }
}