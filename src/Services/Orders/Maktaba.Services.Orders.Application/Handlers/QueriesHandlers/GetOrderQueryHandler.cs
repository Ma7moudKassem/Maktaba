namespace Maktaba.Services.Orders.Application.Handlers.QueriesHandlers;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
{
    public Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}