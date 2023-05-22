namespace Maktaba.Services.Basket.Infrastructure;

public class UserService : IUserService
{
    private IHttpContextAccessor _httpContext;
    public UserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public string GetUserIdentity() =>
         _httpContext?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
         throw new ArgumentNullException(nameof(_httpContext));
}