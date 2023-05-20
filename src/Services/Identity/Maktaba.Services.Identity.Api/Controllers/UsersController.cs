namespace Maktaba.Services.Identity.Api;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    //Get api/v1/users?userName={user name}
    [HttpGet]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUserByUserName([FromQuery] string userName)
    {
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
            await _repository.UpdateUserAsync(model);

            return NoContent();
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}