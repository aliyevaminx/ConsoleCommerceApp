namespace Data.Repository.Concrete;

public class SellerRepository : Repository<Seller>, ISellerRepository
{
    private readonly AppDbContext _context;

    public SellerRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Seller GetSellerByEmail(string email)
    {
        return _context.Sellers.FirstOrDefault(s => s.Email == email);
    }
}
