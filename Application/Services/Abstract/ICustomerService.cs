namespace Application.Services.Abstract;

public  interface ICustomerService
{
    void BuyProduct(Customer customer);
    void ShowPurchasedProducts(Customer customer);
    void ShowPurchasedProductsByDate(Customer customer);
    void SearchProduct();
}
