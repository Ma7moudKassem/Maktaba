namespace Maktaba.Services.Identity.Api;

[Route("api/[controller]")]
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

    [HttpGet]
    public async Task<IActionResult> GetUserByUserName([FromQuery] string userName)
    {
        User? user = await _repository.GetUserByUserNameAsync(userName);

        if (user is null)
            return NotFound($"User with user name: {userName} is not found");

        UserDto dto = _mapper.Map<UserDto>(user);

        return Ok(dto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromQuery] RegisterModel model)
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