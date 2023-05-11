namespace Maktaba.Services.Identity.Domain;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(x => x.Name, 
            e => e.MapFrom(n => string.Join(' ', n.FirstName, n.LastName)));
    }
}