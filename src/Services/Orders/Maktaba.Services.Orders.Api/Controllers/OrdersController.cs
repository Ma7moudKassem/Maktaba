namespace Maktaba.Services.Orders.Api;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repository;

    public OrdersController(IOrderRepository repository)
    {
    }
}