namespace Maktaba.Services.Catalog.Application;

public record DeleteBookCommand(Guid Id) : IRequest;