namespace Maktaba.Services.Catalog.Application;

public record AddBookCommand(Book Book) : IRequest;