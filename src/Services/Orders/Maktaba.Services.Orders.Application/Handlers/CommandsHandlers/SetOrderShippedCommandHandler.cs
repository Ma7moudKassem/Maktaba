namespace Maktaba.Services.Orders.Application.Handlers.CommandsHandlers;

public class SetOrderShippedCommandHandler : IRequestHandler<SetOrderShippedCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<SetOrderShippedCommandHandler> _logger;
    private readonly IOrderIntegrationEventService _integrationEventService;
    public SetOrderShippedCommandHandler(IOrderRepository repository,
        ILogger<SetOrderShippedCommandHandler> logger,
        IOrderIntegrationEventService integrationEventService)
    {
        _repository = repository;
        _logger = logger;
        _integrationEventService = integrationEventService;
    }

    public async Task Handle(SetOrderShippedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Setting order with id: {OrderId} shipped", request.OrderId);

            var order = await _repository.GetOrderAsync(request.OrderId, cancellationToken) ??
                throw new OrderNotProvidedException(request.OrderId);

            order.SetOrderShipped();

            await _repository.UpdateOrderAsync(order, cancellationToken);

            var @event = new SetOrderShippedIntegrationEvent(request.OrderId);

            try
            {
                _logger.LogInformation("Publishing IntegrationEvent: {EventId} - ({@IntegrstionEvent})",
                    @event.Id,
                    @event);

                _integrationEventService.PublishThroughEventBus(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing IntegrationEvent: {EventId} - ({@IntegrstionEvent})",
                    @event.Id,
                    @event);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Setting order with id: {OrderId} shipped", request.OrderId);
            throw;
        }
    }
}