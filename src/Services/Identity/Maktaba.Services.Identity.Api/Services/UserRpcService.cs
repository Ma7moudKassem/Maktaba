namespace Maktaba.Services.Identity.Api;

public class UserRpcService : IdentityServicesBase
{
    private readonly IUserRepository _userRepository;
    public UserRpcService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task<gRPC.User> GetUser(UserName request, ServerCallContext context)
    {
        Domain.User userFromDb = await _userRepository.GetUserByUserNameAsync(request.UserName_) ??
            throw new UserNotProvidedException(request.UserName_);

        gRPC.User user = new()
        {
            Name = string.Join(' ', userFromDb.FirstName, userFromDb.LastName),
            Email = userFromDb.Email,
            FullAddress = userFromDb.FullAddress,
            PhoneNumber = userFromDb.PhoneNumber,
            UserName = userFromDb.UserName,
        };

        return user;
    }
}
