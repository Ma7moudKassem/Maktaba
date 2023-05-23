namespace Maktaba.Services.Catalog.Application;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand>
{
    private readonly IBookRepository _repository;
    public AddBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(AddBookCommand request, CancellationToken cancellationToken) =>
        await _repository.AddBookAsync(request.Book);
}