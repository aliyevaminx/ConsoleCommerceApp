namespace Application.Services.Abstract;

public interface IAdminService
{
    void ShowAllCustomers();
    void ShowAllSellers();
    void ShowAllOrders();
    void ShowSellerOrders();
    void ShowCustomerOrders();
    void ShowOrdersByDate();
    void AddSeller();
    void AddCustomer();
    void AddCategory();
    void DeleteSeller();
    void DeleteCustomer();
}
