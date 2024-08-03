namespace Core.Constants;


public enum LoginOptions
{
    Admin,
    Customer,
    Seller
}

public enum AdminOptions
{
    Exit,
    ShowAllCustomers,
    ShowAllSellers,
    ShowAllOrders,
    ShowOrderByDate,
    ShowCustomerOrder,
    ShowSellerOrder,
    AddCustomer,
    AddSeller,
    AddCategory,
    DeleteCustomer,
    DeleteSeller,
    DeleteCategory,
    LoginMenu
}

public enum CustomerOptions
{
    Exit,
    ShowPurchasedProducts,
    ShowPurchasedProductsByDate,
    SearchProduct,
    BuyProduct,
    LoginMenu
}

public enum SellerOptions
{
    Exit,
    ShowSoldProducts,
    ShowSoldProductsByDate,
    ShowIncome,
    AddProduct,
    ChangeCountOfProduct,
    DeleteProduct,
    LoginMenu
}