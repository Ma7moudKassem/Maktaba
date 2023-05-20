namespace Maktaba.Services.Identity.Api;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMessageBus _bus;

    public AuthController(IAuthService authService, IMessageBus bus)
    {
        _authService = authService;
        _bus = bus;
    }

    //Post api/v1/auth/register
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        AuthModel result = await _authService.RegisterAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest();

        if (result.RefreshToken is not null)
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        await _bus.PublishMessage(message: result, queueName: "user-queue");

        return Ok(result);
    }

    //Post api/v1/auth/logIn
    [HttpPost("logIn")]
    [ProducesResponseType(typeof(AuthModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
    {
        AuthModel result = await _authService.GetTokenAsync(model);

        if (!result.IsAuthenticated)
            return BadRequest();

        if (!string.IsNullOrEmpty(result.RefreshToken))
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        return Ok(result);
    }

    //Post api/v1/auth/addRole
    [HttpPost("addRole")]
    [ProducesResponseType(typeof(AddRoleModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        string result = await _authService.AddRoleAsync(model);

        if (!string.IsNullOrEmpty(result))
            return BadRequest();

        return Ok(model);
    }

    //Get api/v1/auth/refreshToken
    [HttpGet("refreshToken")]
    [ProducesResponseType(typeof(AuthModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RefreshToken()
    {
        string? refreshToken = Request.Cookies["refreshToken"];

        AuthModel result = new();
        if (refreshToken is not null)
            result = await _authService.RefreshTokenAsync(refreshToken);

        if (!result.IsAuthenticated)
            return BadRequest();

        if (result.RefreshToken is not null)
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

        return Ok(result);
    }

    //Post api/v1/auth/revokeToken
    [HttpPost("revokeToken")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
    {
        string? token = model.Token ?? Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
            return BadRequest();

        bool result = await _authService.RevokeTokenAsync(token);

        if (!result)
            return BadRequest();

        return Ok();
    }

    private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
    {
        CookieOptions cookieOptions = new()
        {
            HttpOnly = true,
            Expires = expires.ToLocalTime(),
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}
