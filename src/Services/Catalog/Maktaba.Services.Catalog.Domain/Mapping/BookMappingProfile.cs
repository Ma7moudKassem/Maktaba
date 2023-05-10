namespace Maktaba.Services.Catalog.Domain;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(x => x.CategoryName,
                e => e.MapFrom(c => c.Category.Name));
    }
}