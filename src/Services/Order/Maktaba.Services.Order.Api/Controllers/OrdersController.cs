namespace Maktaba.Services.Order.Api;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly IUserServices _userServices;

    public OrdersController(IOrderRepository repository, IUserServices userServices)
    {
        _repository = repository;
        _userServices = userServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync()
    {

        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync([FromRoute] Guid id)
    {
        Domain.Order order = await _repository.GetOrderAsync(id) ??
            throw new OrderNotProvidedException();

        User user = await _userServices.GetUserAsync(order.UserName) ??
            throw new UserNotProvidedException(order.UserName);

        order.User = user;

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> PostOrderAsync(Domain.Order order)
    {
        try
        {
            await _repository.AddOrderAsync(order);

            return StatusCode(201);
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

}