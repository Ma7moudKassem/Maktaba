namespace Maktaba.Services.Catalog.Infrastructure;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(CatalogDbContext context) : base(context) { }
}