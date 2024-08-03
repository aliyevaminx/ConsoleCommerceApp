namespace Data.Repository.Concrete;

public class AdminRepository : Repository<Admin>, IAdminRepository
{
    private readonly AppDbContext _context;
    public AdminRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Admin GetAdminByEmail(string email)
    {
        return _context.Admins.FirstOrDefault(a => a.Email == email);
    }
}
