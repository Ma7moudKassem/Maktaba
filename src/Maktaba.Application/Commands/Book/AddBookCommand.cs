namespace Maktaba.Application;

public record AddBookCommand(Book Book) : IRequest;