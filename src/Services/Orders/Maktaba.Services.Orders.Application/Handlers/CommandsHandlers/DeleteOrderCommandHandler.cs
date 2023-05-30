namespace Maktaba.Services.Orders.Application.Handlers.CommandsHandlers;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository repository,
        ILogger<DeleteOrderCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Deleting order with id: {OrderId}", request.Id);

            await _repository.DeleteOrderAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Deleting order with id: {OrderId}", request.Id);
            throw;
        }
    }

}