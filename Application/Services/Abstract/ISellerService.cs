namespace Application.Services.Abstract;

public interface ISellerService
{
    void ShowSoldProducts(Seller seller);
    void ShowSoldProductsByDate(Seller seller);
    void SearchProduct(Seller seller);
    void ShowIncome(Seller seller);
    void AddProduct(Seller seller);
    void ChangeCountOfProduct(Seller seller);
    void DeleteProduct(Seller seller);
   
}
