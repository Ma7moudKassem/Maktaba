namespace Maktaba.Services.Orders.Application.Commands;

public record SetOrderShippedCommand(Guid orderId) : IRequest;