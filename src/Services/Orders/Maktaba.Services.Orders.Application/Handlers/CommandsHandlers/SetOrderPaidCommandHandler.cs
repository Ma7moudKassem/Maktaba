namespace Maktaba.Services.Orders.Application.Handlers.CommandsHandlers;

public class SetOrderPaidCommandHandler : IRequestHandler<SetOrderPaidCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<SetOrderPaidCommandHandler> _logger;
    private readonly IOrderIntegrationEventService _integrationEventService;
    public SetOrderPaidCommandHandler(IOrderRepository repository,
        ILogger<SetOrderPaidCommandHandler> logger,
        IOrderIntegrationEventService integrationEventService)
    {
        _repository = repository;
        _logger = logger;
        _integrationEventService = integrationEventService;
    }

    public async Task Handle(SetOrderPaidCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Setting order with id: {OrderId} paid", request.OrderId);

            var order = await _repository.GetOrderAsync(request.OrderId, cancellationToken) ??
                throw new OrderNotProvidedException(request.OrderId);

            order.SetOrderPaid();

            await _repository.UpdateOrderAsync(order, cancellationToken);

            var @event = new SetOrderPaidIntegrationEvent(request.OrderId);

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
            _logger.LogError(ex, "ERROR Setting order with id: {OrderId} paid", request.OrderId);
            throw;
        }
    }
}