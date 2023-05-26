namespace Maktaba.Services.Orders.Application.Commands;

public record UpdateOrderCommand(Order Order) : IRequest;