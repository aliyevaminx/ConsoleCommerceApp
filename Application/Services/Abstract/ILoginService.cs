namespace Application.Services.Abstract;

public interface ILoginService
{
    Admin IsAdminLoggedIn();
    Customer IsCustomerLoggedIn();
    Seller IsSellerLoggedIn();
}
