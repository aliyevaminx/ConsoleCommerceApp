namespace Data.Repository.Abstract;

public interface IOrderRepository : IRepository<Order>
{
    List<Order> GetAllOrdersByCustomer(int id);
    List<Order> GetAllOrdersBySeller(int id);

}
