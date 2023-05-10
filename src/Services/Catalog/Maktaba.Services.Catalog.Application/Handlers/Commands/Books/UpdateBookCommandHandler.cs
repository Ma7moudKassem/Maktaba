namespace Maktaba.Services.Catalog.Application;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository _repository;
    public UpdateBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.UpdateAsync(request.Book, cancellationToken);
        }
        catch (Exception exception)
        {
            Log.Error(exception.GetExceptionErrorSimplified());
            throw;
        }
    }
}