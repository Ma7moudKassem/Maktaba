namespace Maktaba.Services.Orders.Application.Handlers.CommandsHandlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderRepository repository,
        ILogger<CreateOrderCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Creating order ({@Order})", request.Order);

            await _repository.AddOrderAsync(request.Order, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Creating order ({@Order})", request.Order);
            throw;
        }
    }
}