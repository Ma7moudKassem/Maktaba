namespace Maktaba.Services.Orders.Application.Handlers.CommandsHandlers;

public class SetOrderSubmittedCommandHandler : IRequestHandler<SetOrderSubmittedCommand>
{
    private readonly IOrderRepository _repository;
    private readonly ILogger<SetOrderSubmittedCommandHandler> _logger;
    private readonly IOrderIntegrationEventService _integrationEventService;
    public SetOrderSubmittedCommandHandler(IOrderRepository repository,
        ILogger<SetOrderSubmittedCommandHandler> logger,
        IOrderIntegrationEventService integrationEventService)
    {
        _repository = repository;
        _logger = logger;
        _integrationEventService = integrationEventService;
    }

    public async Task Handle(SetOrderSubmittedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Setting order with id: {OrderId} submitted", request.OrderId);

            var order = await _repository.GetOrderAsync(request.OrderId, cancellationToken) ??
                throw new OrderNotProvidedException(request.OrderId);

            order.SetOrderSubmitted();

            await _repository.UpdateOrderAsync(order, cancellationToken);

            var @event = new SetOrderSubmittedIntegrationEvent(request.OrderId);

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
            _logger.LogError(ex, "ERROR Setting order with id: {OrderId} submitted", request.OrderId);
            throw;
        }
    }
}