namespace Maktaba.Services.Orders.Application.Commands;

public record SetOrderPaidCommand(Guid OrderId) : IRequest;