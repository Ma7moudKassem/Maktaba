namespace Maktaba.Application;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _repository;
    public DeleteBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
        }
        catch (Exception exception)
        {
            Log.Error(exception.GetExceptionErrorSimplified());
            throw;
        }
    }
}