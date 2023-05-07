namespace Maktaba.Infrastructure;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MaktabaDbContext context) : base(context) { }
}