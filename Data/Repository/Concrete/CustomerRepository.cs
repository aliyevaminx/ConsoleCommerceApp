using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Concrete;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Customer GetCustomerByEmail(string email) => _context.Customers.FirstOrDefault(c => c.Email == email);
}
