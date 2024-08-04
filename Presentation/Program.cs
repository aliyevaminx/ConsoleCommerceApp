using Application.Services.Concrete;
using Core.Constants;
using Data.UnitOfWork.Concrete;

public static class Program
{
    private static readonly UnitOfWork _unitOfWork;
    private static readonly AdminService _adminService;
    private static readonly CustomerService _customerService;
    private static readonly SellerService _sellerService;
    private static readonly LoginService _loginService;

    static Program()
    {
        _unitOfWork = new UnitOfWork();
        _adminService = new AdminService(_unitOfWork);
        _customerService = new CustomerService(_unitOfWork);
        _sellerService = new SellerService(_unitOfWork);
        _loginService = new LoginService(_unitOfWork);
    }

    public static void Main(string[] args)
    {
    LoginMenuLine: LoginMenu();

        string optionInput = Console.ReadLine();
        int option;
        bool isTrueOption = int.TryParse(optionInput, out option);

        if (isTrueOption)
        {
            if (option == 0)
            {
            LoggedInLine: var loggedIn = _loginService.IsAdminLoggedIn();

                if (loggedIn is not null)
                {
                    while (true)
                    {
                        ShowAdminMenu();

                        var adminOptionInput = Console.ReadLine();
                        int adminOption;
                        isTrueOption = int.TryParse(adminOptionInput, out adminOption);

                        if (isTrueOption)
                        {
                            switch ((AdminOptions)adminOption)
                            {
                                case AdminOptions.ShowAllCustomers:
                                    _adminService.ShowAllCustomers();
                                    break;
                                case AdminOptions.ShowAllSellers:
                                    _adminService.ShowAllSellers();
                                    break;
                                case AdminOptions.ShowAllOrders:
                                    _adminService.ShowAllOrders();
                                    break;
                                case AdminOptions.ShowOrderByDate:
                                    _adminService.ShowOrdersByDate();
                                    break;
                                case AdminOptions.ShowCustomerOrder:
                                    _adminService.ShowCustomerOrders();
                                    break;
                                case AdminOptions.ShowSellerOrder:
                                    _adminService.ShowSellerOrders();
                                    break;
                                case AdminOptions.AddCustomer:
                                    _adminService.AddCustomer();
                                    break;
                                case AdminOptions.AddSeller:
                                    _adminService.AddSeller();
                                    break;
                                case AdminOptions.AddCategory:
                                    _adminService.AddCategory();
                                    break;
                                case AdminOptions.DeleteCustomer:
                                    _adminService.DeleteCustomer();
                                    break;
                                case AdminOptions.DeleteSeller:
                                    _adminService.DeleteSeller();
                                    break;
                                case AdminOptions.DeleteCategory:
                                    _adminService.DeleteCategory();
                                    break;
                                case AdminOptions.Exit:
                                    return;
                                case AdminOptions.LoginMenu:
                                    goto LoginMenuLine;
                                default:
                                    Messages.InvalidInputMessage("Option");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Messages.WrongInputMessage();
                    goto LoggedInLine;
                }
            }
            else if (option == 1)
            {
            LoggedInLine: var loggedIn = _loginService.IsCustomerLoggedIn();

                if (loggedIn is not null)
                {
                    while (true)
                    {
                        ShowCustomerMenu();
                        var customerOptionInput = Console.ReadLine();
                        int customerOption;
                        isTrueOption = int.TryParse(customerOptionInput, out customerOption);

                        if (isTrueOption)
                        {
                            switch ((CustomerOptions)customerOption)
                            {
                                case CustomerOptions.ShowPurchasedProducts:
                                    _customerService.ShowPurchasedProducts(loggedIn);
                                    break;
                                case CustomerOptions.ShowPurchasedProductsByDate:
                                    _customerService.ShowPurchasedProductsByDate(loggedIn);
                                    break;
                                case CustomerOptions.SearchProduct:
                                    _customerService.SearchProduct();
                                    break;
                                case CustomerOptions.BuyProduct:
                                    _customerService.BuyProduct(loggedIn);
                                    break;
                                case CustomerOptions.Exit:
                                    return;
                                case CustomerOptions.LoginMenu:
                                    goto LoginMenuLine;
                                default:
                                    Messages.InvalidInputMessage("Option");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Messages.WrongInputMessage();
                    goto LoggedInLine;
                }

            }
            else if (option == 2)
            {
            LoggedInLine: var loggedIn = _loginService.IsSellerLoggedIn();

                if (loggedIn is not null)
                {
                    while (true)
                    {
                        ShowSellerMenu();
                        var sellerOptionInput = Console.ReadLine();
                        int sellerOption;
                        isTrueOption = int.TryParse(sellerOptionInput, out sellerOption);

                        if (isTrueOption)
                        {
                            switch ((SellerOptions)sellerOption)
                            {
                                case SellerOptions.ShowSoldProducts:
                                    _sellerService.ShowSoldProducts(loggedIn);
                                    break;
                                case SellerOptions.ShowSoldProductsByDate:
                                    _sellerService.ShowSoldProductsByDate(loggedIn);
                                    break;
                                case SellerOptions.ShowIncome:
                                    _sellerService.ShowIncome(loggedIn);
                                    break;
                                case SellerOptions.AddProduct:
                                    _sellerService.AddProduct(loggedIn);
                                    break;
                                case SellerOptions.ChangeCountOfProduct:
                                    _sellerService.ChangeCountOfProduct(loggedIn);
                                    break;
                                case SellerOptions.DeleteProduct:
                                    _sellerService.DeleteProduct(loggedIn);
                                    break;
                                case SellerOptions.Exit:
                                    return;
                                case SellerOptions.LoginMenu:
                                    goto LoginMenuLine;
                                default:
                                    Messages.InvalidInputMessage("Option");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Messages.WrongInputMessage();
                    goto LoggedInLine;
                }
            }
            else
            {
                Messages.InvalidInputMessage("Option");
                return;
            }
        }
        else
            Messages.InvalidInputMessage("Option");
    }

    public static void LoginMenu()
    {
        Console.WriteLine("0. Admin");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Seller");
    }

    public static void ShowAdminMenu()
    {
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Show All Customers");
        Console.WriteLine("2. Show All Sellers");
        Console.WriteLine("3. Show All Orders");
        Console.WriteLine("4. Show Order By Date");
        Console.WriteLine("5. Show Customer's Orders");
        Console.WriteLine("6. Show Seller's Orders");
        Console.WriteLine("7. Add Customer");
        Console.WriteLine("8. Add Seller");
        Console.WriteLine("9. Add Category");
        Console.WriteLine("10. Delete Customer");
        Console.WriteLine("11. Delete Seller");
        Console.WriteLine("12. Delete Category");
        Console.WriteLine("13. Login Menu");
    }

    public static void ShowCustomerMenu()
    {
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Show Purchased Products");
        Console.WriteLine("2. Show Purchased Products By Date");
        Console.WriteLine("3. Search Product");
        Console.WriteLine("4. Buy Product");
        Console.WriteLine("5. Login Menu");
    }

    public static void ShowSellerMenu()
    {
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Show Sold Products");
        Console.WriteLine("2. Show Sold Products By Date");
        Console.WriteLine("3. Show Income");
        Console.WriteLine("4. Add Product");
        Console.WriteLine("5. Change Count Of Product");
        Console.WriteLine("6. Delete Product");
        Console.WriteLine("7. Login Menu");
    }
}
