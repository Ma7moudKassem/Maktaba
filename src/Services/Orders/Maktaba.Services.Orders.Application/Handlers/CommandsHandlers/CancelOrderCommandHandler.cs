namespace Maktaba.Services.Orders.Application.Handlers.CommandsHandlers;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<CancelOrderCommandHandler> _logger;
    private readonly IOrderIntegrationEventService _eventService;

    public CancelOrderCommandHandler(ILogger<CancelOrderCommandHandler> logger,
        IOrderRepository repository,
        IOrderIntegrationEventService eventService)
    {
        _logger = logger;
        _repository = repository;
        _eventService = eventService;
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Canceling order with id: {OrderId}", request.OrderId);
            Order? order = await _repository.GetOrderAsync(request.OrderId, cancellationToken) ??
                throw new OrderNotProvidedException(request.OrderId);

            order.CancelOrder();

            await _repository.UpdateOrderAsync(order, cancellationToken);

            OrderHasCancelledIntegrationEvent @event = new(
                orderId: order.Id,
                userId: order.UserId,
                userName: order.User.Name,
                street: order.Address.Street,
                city: order.Address.City,
                country: order.Address.Country);
            try
            {
                _logger.LogInformation("Publishing Integration Event: {EventId} - ({@Event})", @event.Id, @event);

                _eventService.PublishThroughEventBus(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERORR Publishing Integration Event: {EventId} - ({@Event})", @event.Id, @event);
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Canceling order with id: {OrderId}", request.OrderId);
            throw;
        }
    }
}