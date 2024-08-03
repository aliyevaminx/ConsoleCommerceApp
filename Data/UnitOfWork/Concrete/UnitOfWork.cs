using Core.Constants;
using Data.Contexts;
using Data.Repository.Concrete;
using Data.UnitOfWork.Abstract;

namespace Data.UnitOfWork.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public readonly AdminRepository Admins;
    public readonly CategoryRepository Categories;
    public readonly CustomerRepository Customers;
    public readonly OrderRepository Orders;
    public readonly ProductRepository Products;
    public readonly SellerRepository Sellers;

    public UnitOfWork()
    {
        _context = new AppDbContext();
        Admins = new AdminRepository(_context);
        Categories = new CategoryRepository(_context);
        Customers = new CustomerRepository(_context);
        Orders = new OrderRepository(_context);
        Products = new ProductRepository(_context);
        Sellers = new SellerRepository(_context);
    }

    public void Commit(string title, string process)
    {
        try
        {
            _context.SaveChanges();
            Messages.SuccessMessage(title, process);
        }
        catch (Exception)
        {
            Messages.ErrorHasOccuredMessage();
        }
    }
}
