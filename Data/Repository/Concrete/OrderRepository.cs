using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Concrete;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Order> GetAllOrdersByCustomer(int id)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Seller)
            .Include(o => o.Product)
            .Where(o => o.CustomerId == id).ToList();
    }

    public List<Order> GetAllOrdersBySeller(int id)
    {
        return _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Seller)
            .Include(o => o.Product)
            .Where(o => o.SellerId == id).ToList();
    }

    public int GetCountOfOrders(List<Order> orders) => orders.Count();
}
