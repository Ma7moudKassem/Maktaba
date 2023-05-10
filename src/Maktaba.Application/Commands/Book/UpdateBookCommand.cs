namespace Maktaba.Application;

public record UpdateBookCommand(Book Book) : IRequest;