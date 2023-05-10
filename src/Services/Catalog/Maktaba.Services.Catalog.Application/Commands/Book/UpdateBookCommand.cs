namespace Maktaba.Services.Catalog.Application;

public record UpdateBookCommand(Book Book) : IRequest;