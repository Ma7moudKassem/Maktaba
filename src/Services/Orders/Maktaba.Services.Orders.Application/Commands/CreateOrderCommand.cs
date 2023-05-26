namespace Maktaba.Services.Orders.Application.Commands;

public record CreateOrderCommand(Order Order) : IRequest;