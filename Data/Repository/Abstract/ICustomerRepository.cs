namespace Data.Repository.Abstract;

public interface ICustomerRepository : IRepository<Customer>
{
    Customer GetCustomerByEmail(string email);
}
