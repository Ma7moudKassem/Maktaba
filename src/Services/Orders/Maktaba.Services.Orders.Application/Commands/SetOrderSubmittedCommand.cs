namespace Maktaba.Services.Orders.Application.Commands;

public record SetOrderSubmittedCommand(Guid OrderId) : IRequest;