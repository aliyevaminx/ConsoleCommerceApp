namespace Data.Repository.Concrete;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context): base(context)
    {
        _context = context;
    }

    public Product GetByName(string name)
    {
        return _context.Products.FirstOrDefault(p => p.Name == name);
    }

    public List<Product> GetProductBySubstring(string substring)
    {
        return _context.Products.Where(p => p.Name.ToLower().Contains(substring.ToLower())).ToList();
    }
}
