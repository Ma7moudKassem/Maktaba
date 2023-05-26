namespace Maktaba.Services.Orders.Application.Commands;

public record DeleteOrderCommand(Guid Id) : IRequest;