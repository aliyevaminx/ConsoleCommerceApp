namespace Data.Repository.Concrete;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Category GetByName(string name)
    {
        return _context.Categories.FirstOrDefault(c => c.Name == name);
    }
}
