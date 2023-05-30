namespace Maktaba.Services.Orders.Application.Handlers.CommandsHandlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository repository,
        ILogger<UpdateOrderCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Updating order ({@Order})", request.Order);

            await _repository.UpdateOrderAsync(request.Order, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Updating order ({@Order})", request.Order);
            throw;
        }
    }
}