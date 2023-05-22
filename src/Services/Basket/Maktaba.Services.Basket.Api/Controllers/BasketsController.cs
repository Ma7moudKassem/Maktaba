namespace Maktaba.Services.Basket.Api;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _repository;
    private readonly IUserService _userService;
    private readonly IEventBus _eventBus;
    private readonly ILogger<BasketsController> _logger;

    public BasketsController(IBasketRepository repository,
        IEventBus eventBus,
        ILogger<BasketsController> logger,
        IUserService userService)
    {
        _repository = repository;
        _eventBus = eventBus;
        _logger = logger;
        _userService = userService;
    }

    //GET api/v1/baskets/id
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BasketCheckout), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BasketCheckout>> GetBasketById([FromRoute] string identity)
    {
        _logger.LogInformation("getting basket checkout with id: {Id}", identity);

        var basket = await _repository.GetBasketAsync(identity);

        if (basket is null)
            return NotFound($"Basket checkout with id: {identity} is not found");

        return Ok(basket);
    }

    //POST api/v1/baskets
    [HttpPost]
    [ProducesResponseType(typeof(UserBasket), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserBasket>> AddBasketAsync([FromBody] UserBasketDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        string userIdentity = _userService.GetUserIdentity();

        UserBasket customerBasket = new()
        {
            UserIdentity = userIdentity,
            Items = dto.Items,
        };

        var basketToAdd = await _repository.AddBasketAsync(customerBasket);

        if (basketToAdd is null)
            return BadRequest();

        return Ok(basketToAdd);
    }

    //POST api/v1/baskets/checkout
    [HttpPost("checkout")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string userIdentity = _userService.GetUserIdentity();

        var basket = await _repository.GetBasketAsync(userIdentity);

        if (basket is null)
            return NotFound($"Basket checkout with id: {userIdentity} is not found");

        var userName = User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;

        CheckoutCompletedEventIntegration eventMessage = new(
            userIdentity: userIdentity,
            userName: userName,
            city: basketCheckout.City,
            street: basketCheckout.Street,
            country: basketCheckout.Country,
            cardNumber: basketCheckout.CardNumber,
            cardHolderName: basketCheckout.CardHolderName,
            cardExpiration: basketCheckout.CardExpiration,
            cardSecurityNumber: basketCheckout.CardSecurityNumber,
            cardTypeId: basketCheckout.CardTypeId,
            buyer: basketCheckout.Buyer,
            basket: basket);
        try
        {
            _logger.LogInformation("Publishing integration event: {IntegrationEventId} to order service to complete order", eventMessage.Id);

            _eventBus.Publish(eventMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId}", eventMessage.Id);
            throw;
        }

        return Accepted();
    }

    // DELETE api/v1/baskets/5
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task DeleteBasketByIdAsync([FromRoute] string userIdentity)
    {
        try
        {
            _logger.LogInformation("Deleting basket with id: {Id}", userIdentity);

            await _repository.DeleteBasketAsync(userIdentity);
        }
        catch (Exception exception)
        {
            _logger.LogError("Error Deleting basket with id: {Id} , exception: {ExceptionMessage}", userIdentity,
                exception.GetExceptionErrorSimplified());

            throw;
        }
    }
}