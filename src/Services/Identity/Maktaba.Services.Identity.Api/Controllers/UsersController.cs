using Maktaba.Services.Identity.gRPC;

namespace Maktaba.Services.Identity.Api;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserRepository repository,
        IMapper mapper,
        ILogger<UsersController> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    //Get api/v1/users?userName={user name}
    [HttpGet]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUserByUserName([FromQuery] string userName)
    {
        _logger.LogInformation("Get User With User Name: {userName}", userName);
        Domain.User? user = await _repository.GetUserByUserNameAsync(userName);

        if (user is null)
            return NotFound($"User with user name: {userName} is not found");

        UserDto dto = _mapper.Map<UserDto>(user);

        return Ok(dto);
    }

    //Put api/v1/users
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateUser(RegisterModel model)
    {
        try
        {
            _logger.LogInformation("Updating User With User Name: {userName}...", model.Username);

            await _repository.UpdateUserAsync(model);

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError("Faild To Update Category with User Name: {userName} , Exception: {exceptionMessage}",
                model.Username,
                exception.GetExceptionErrorSimplified());

            throw new Exception(exception.Message);
        }
    }
}