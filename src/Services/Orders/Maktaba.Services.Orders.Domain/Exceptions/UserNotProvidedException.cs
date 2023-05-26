namespace Maktaba.Services.Orders.Domain;

public sealed class UserNotProvidedException : Exception
{
    public UserNotProvidedException(string userName) :
        base($"User with user name: {userName} is not provided")
    { }
}