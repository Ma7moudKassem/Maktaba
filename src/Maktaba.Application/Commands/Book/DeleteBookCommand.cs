namespace Maktaba.Application;

public record DeleteBookCommand(Guid Id) : IRequest;