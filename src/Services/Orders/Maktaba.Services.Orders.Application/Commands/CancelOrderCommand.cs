namespace Maktaba.Services.Orders.Application.Commands;

public record CancelOrderCommand(Guid OrderId) : IRequest;