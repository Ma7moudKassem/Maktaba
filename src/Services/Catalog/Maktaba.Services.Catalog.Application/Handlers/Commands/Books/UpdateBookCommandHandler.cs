namespace Maktaba.Services.Catalog.Application;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository _repository;
    private readonly ICatalogIntegrationEventService _eventService;
    public UpdateBookCommandHandler(IBookRepository repository, ICatalogIntegrationEventService eventService)
    {
        _repository = repository;
        _eventService = eventService;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        try
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

                _eventService.PublisThroughEventBusAsync(priceChangedEvent);
            }

            await _repository.UpdateBookAsync(request.Book);
        }
        catch (Exception exception)
        {
            Log.Error(exception.GetExceptionErrorSimplified());
            throw;
        }
    }
}