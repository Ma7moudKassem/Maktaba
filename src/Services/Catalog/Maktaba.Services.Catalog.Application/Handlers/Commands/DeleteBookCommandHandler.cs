namespace Maktaba.Services.Catalog.Application;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _repository;
    public DeleteBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken) =>
        await _repository.DeleteBookAsync(request.Id);

}