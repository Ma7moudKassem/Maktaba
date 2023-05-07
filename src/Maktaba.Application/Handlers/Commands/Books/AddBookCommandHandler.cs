namespace Maktaba.Application;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
{
    private readonly IBookRepository _repository;
    public AddBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.AddAsync(request.Book, cancellationToken);
        }
        catch (Exception exception)
        {
            Log.Error(exception.GetExceptionErrorSimplified());
            throw;
        }
    }
}