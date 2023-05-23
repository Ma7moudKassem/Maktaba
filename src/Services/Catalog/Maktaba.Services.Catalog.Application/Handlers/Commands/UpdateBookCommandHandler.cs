namespace Maktaba.Services.Catalog.Application;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository _repository;
    private readonly ICatalogIntegrationEventService _eventService;
    private readonly ILogger<UpdateBookCommandHandler> _logger;
    public UpdateBookCommandHandler(IBookRepository repository,
        ICatalogIntegrationEventService eventService,
        ILogger<UpdateBookCommandHandler> logger)
    {
        _repository = repository;
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {

        Book bookFromDb = await _repository.GetByIdAsync(request.Book.Id) ??
            throw new BookNotProvidedException(request.Book.Id.ToString());

        double oldPrice = bookFromDb.Price;

        bool raiseBookPriceChangedEvent = oldPrice != request.Book.Price;

        if (raiseBookPriceChangedEvent)
        {
            var priceChangedEvent = new BookPriceChangedIntegrationEvent(
                bookId: request.Book.Id,
                oldPrice: oldPrice,
                newPrice: request.Book.Price);

            try
            {
                _logger.LogInformation("Publishing event: {EventName}", priceChangedEvent.GetGenericTypeName());

                _eventService.PublisThroughEventBusAsync(priceChangedEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing event: {EventName}", priceChangedEvent.GetGenericTypeName());

                throw;
            }
        }

        await _repository.UpdateBookAsync(request.Book);
    }
}