namespace Maktaba.Services.Orders.Application.Queries;

public record GetOrderQuery(Guid Id) : IRequest<Order>;