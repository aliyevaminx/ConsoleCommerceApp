using Core.Security;
using System.Globalization;

namespace Application.Services.Concrete;

public class AdminService : IAdminService
{
    private readonly UnitOfWork _unitOfWork;

    public AdminService(UnitOfWork unitofWork)
    {
        _unitOfWork = unitofWork;
    }

    public void ShowAllCustomers()
    {
        if (!_unitOfWork.Customers.GetAll().Any())
        {
            Messages.ThereIsNotMessage("customer");
            return;
        }

        foreach (var customer in _unitOfWork.Customers.GetAll())
            Console.WriteLine($"Id: {customer.Id} Name: {customer.Name} Surname: {customer.Surname} Email: {customer.Email} " +
                              $"Phone: {customer.PhoneNumber} Serial Number: {customer.SerialNumber}");
    }
    public void ShowAllSellers()
    {
        if (!_unitOfWork.Sellers.GetAll().Any())
        {
            Messages.ThereIsNotMessage("seller");
            return;
        }

        foreach (var seller in _unitOfWork.Sellers.GetAll())
            Console.WriteLine($"Id: {seller.Id} Name: {seller.Name} Surname: {seller.Surname} Email: {seller.Email} " +
                              $"Phone: {seller.PhoneNumber} Serial Number: {seller.SerialNumber}");
    }
    public void ShowAllOrders()
    {
        if (!_unitOfWork.Orders.GetAll().Any())
        {
            Messages.ThereIsNotMessage("order");
            return;
        }

        foreach (var order in _unitOfWork.Orders.GetAll().OrderByDescending(o => o.OrderTime))
        {
            var seller = _unitOfWork.Sellers.Get(order.SellerId);
            var customer = _unitOfWork.Customers.Get(order.CustomerId);
            Console.WriteLine($"Id: {order.Id} Total: {order.totalAmount} Product Count: {order.productCount} " +
                $"Seller: {seller.Name} {seller.Surname} {seller.SerialNumber} " +
                $"Customer: {customer.Name} {customer.Surname} {customer.SerialNumber} Order Time: {order.OrderTime}");
        }
    }
    public void ShowOrdersByDate()
    {
        if (!_unitOfWork.Orders.GetAll().Any())
        {
            Messages.ThereIsNotMessage("order");
            return;
        }

        Messages.InputMessage("order time (dd.MM.yyyy)");
        string orderTimeInput = Console.ReadLine();
        DateTime orderTime;
        bool isTrueFormat = DateTime.TryParseExact(orderTimeInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out orderTime);
        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("Order time");
            return;
        }

        bool existOrder = false;

        foreach (var order in _unitOfWork.Orders.GetAll())
        {
            if (order.OrderTime.ToString("dd.MM.yyyy") == orderTime.ToString("dd.MM.yyyy"))
            {
                existOrder = true;
                var seller = _unitOfWork.Sellers.Get(order.SellerId);
                var customer = _unitOfWork.Customers.Get(order.CustomerId);

                Console.WriteLine($"Id: {order.Id} Total: {order.totalAmount} Product Count: {order.productCount} " +
               $"Seller: {seller.Name} {seller.Surname} {seller.SerialNumber} " +
               $"Customer: {customer.Name} {customer.Surname} {customer.SerialNumber} Order Time: {order.OrderTime}");
            }
        }

        if (!existOrder)
            Messages.NotFoundMessage("Order");
    }
    public void ShowCustomerOrders()
    {
        if (!_unitOfWork.Orders.GetAll().Any())
        {
            Messages.ThereIsNotMessage("order");
            return;
        }

        ShowAllCustomers();
        Messages.InputMessage("customer ID to see orders");
        string customerIdInput = Console.ReadLine();
        int customerId;
        bool isTrueFormat = int.TryParse(customerIdInput, out customerId);
        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("Customer ID");
            return;
        }

        bool existOrder = false;

        foreach (var order in _unitOfWork.Orders.GetAll())
        {
            if (order.CustomerId == customerId)
            {
                existOrder = true;
                var seller = _unitOfWork.Sellers.Get(order.SellerId);

                Console.WriteLine($"Id: {order.Id} Total: {order.totalAmount} Product Count: {order.productCount} " +
               $"Seller: {seller.Name} {seller.Surname} {seller.SerialNumber} Order Time: {order.OrderTime}");
            }
        }

        if (!existOrder)
            Messages.NotFoundMessage("Customer order");
    }
    public void ShowSellerOrders()
    {
        if (!_unitOfWork.Orders.GetAll().Any())
        {
            Messages.ThereIsNotMessage("order");
            return;
        }

        ShowAllSellers();
        Messages.InputMessage("seller ID to see orders");
        string sellerIdInput = Console.ReadLine();
        int sellerId;
        bool isTrueFormat = int.TryParse(sellerIdInput, out sellerId);
        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("Seller ID");
            return;
        }

        bool existOrder = false;

        foreach (var order in _unitOfWork.Orders.GetAll())
        {
            if (order.SellerId == sellerId)
            {
                existOrder = true;
                var customer = _unitOfWork.Customers.Get(order.CustomerId);

                Console.WriteLine($"Id: {order.Id} Total: {order.totalAmount} Product Count: {order.productCount} " +
               $"Customer: {customer.Name} {customer.Surname} {customer.SerialNumber} Order Time: {order.OrderTime}");
            }
        }

        if (!existOrder)
            Messages.NotFoundMessage("Seller's order");
    }
    public void AddCategory()
    {
        foreach (var categoryItem in _unitOfWork.Categories.GetAll())
        {
            Console.WriteLine($"Id: {categoryItem.Id} Name: {categoryItem.Name}");
        }

    EnterCategoryNameLine: Messages.InputMessage("category name");
        string categoryName = Console.ReadLine();

        var existName = _unitOfWork.Categories.GetByName(categoryName);
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            Messages.InvalidInputMessage("Category name");
            goto EnterCategoryNameLine;
        }

        if (existName is not null)
        {
            Messages.AlreadyExistMessage(categoryName);
            return;
        }

        Category category = new Category
        {
            Name = categoryName               
        };

        _unitOfWork.Categories.Add(category);
        _unitOfWork.Commit("Category", "added");
    }
    public void AddCustomer()
    {
    EnterCustomerNameLine: Messages.InputMessage("name");
        string customerName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(customerName))
        {
            Messages.InvalidInputMessage("Name");
            goto EnterCustomerNameLine;
        }

    EnterCustomerSurnameLine: Messages.InputMessage("surname");
        string customerSurname = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(customerSurname))
        {
            Messages.InvalidInputMessage("Surname");
            goto EnterCustomerSurnameLine;
        }

    EnterCustomerEmailLine: Messages.InputMessage("email");
        string customerEmail = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(customerEmail) || !customerEmail.IsValidEmail())
        {
            Messages.InvalidInputMessage("Email");
            goto EnterCustomerEmailLine;
        }

        var existEmail = _unitOfWork.Customers.GetCustomerByEmail(customerEmail);

        if (existEmail is not null)
        {
            Messages.AlreadyExistMessage("This email");
            goto EnterCustomerEmailLine;
        }

    EnterCustomerPhoneNumberLine: Messages.InputMessage("phone number");
        string phoneNumber = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            Messages.InvalidInputMessage("Phone number");
            goto EnterCustomerPhoneNumberLine;
        }

    EnterCustomerPasswordLine: Messages.InputMessage("pin (at least 8 characters long)");
        string customerPin = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(customerPin))
        {
            Messages.InvalidInputMessage("Pin");
            goto EnterCustomerPasswordLine;
        }

        if (!customerPin.isValidPassword())
        {
            Messages.InvalidInputMessage("Pin");
            goto EnterCustomerPasswordLine;
        }

        byte[] saltedPassword;
        string hashedPassword = PasswordHasher.HashPassword(customerPin, out saltedPassword);

    EnterCustomerSerialNumberLine: Messages.InputMessage("Serial Number");
        string customerSerialNumber = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(customerSerialNumber))
        {
            Messages.InvalidInputMessage("Serial Number");
            goto EnterCustomerSerialNumberLine;
        }

        Customer customer = new Customer
        {
            Name = customerName,
            Surname = customerSurname,
            Email = customerEmail,
            PhoneNumber = phoneNumber,
            Password = hashedPassword,
            SerialNumber = customerSerialNumber,
            Salt = saltedPassword
        };

        _unitOfWork.Customers.Add(customer);
        _unitOfWork.Commit("Customer", "added");
    }
    public void AddSeller()
    {
    EnterSellerNameLine: Messages.InputMessage("name");
        string sellerName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(sellerName))
        {
            Messages.InvalidInputMessage("Name");
            goto EnterSellerNameLine;
        }

    EnterSellerSurnameLine: Messages.InputMessage("surname");
        string sellerSurname = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(sellerSurname))
        {
            Messages.InvalidInputMessage("Surname");
            goto EnterSellerSurnameLine;
        }

    EnterSellerEmailLine: Messages.InputMessage("email");
        string sellerEmail = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(sellerEmail) || !sellerEmail.IsValidEmail())
        {
            Messages.InvalidInputMessage("Email");
            goto EnterSellerEmailLine;
        }

        var existEmail = _unitOfWork.Sellers.GetSellerByEmail(sellerEmail);

        if (existEmail is not null)
        {
            Messages.AlreadyExistMessage("This email");
            goto EnterSellerEmailLine;
        }

    EnterSellerPhoneNumberLine: Messages.InputMessage("phone number");
        string phoneNumber = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            Messages.InvalidInputMessage("Phone number");
            goto EnterSellerPhoneNumberLine;
        }

    EnterSellerPasswordLine: Messages.InputMessage("pin");
        string sellerPin = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(sellerPin))
        {
            Messages.InvalidInputMessage("Pin");
            goto EnterSellerPasswordLine;
        }

        if (!sellerPin.isValidPassword())
        {
            Messages.InvalidInputMessage("Pin");
            goto EnterSellerPasswordLine;
        }

        byte[] saltedPassword;
        string hashedPassword = PasswordHasher.HashPassword(sellerPin, out saltedPassword);

    EnterSellerSerialNumberLine: Messages.InputMessage("Serial Number");
        string sellerSerialNumber = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(sellerSerialNumber))
        {
            Messages.InvalidInputMessage("Serial Number");
            goto EnterSellerSerialNumberLine;
        }

        Seller seller = new Seller
        {
            Name = sellerName,
            Surname = sellerSurname,
            Email = sellerEmail,
            PhoneNumber = phoneNumber,
            Password = hashedPassword,
            SerialNumber = sellerSerialNumber,
            Salt = saltedPassword
        };

        _unitOfWork.Sellers.Add(seller);
        _unitOfWork.Commit("Seller", "added");
    }
    public void DeleteCustomer()
    {
        ShowAllCustomers();
        if (!_unitOfWork.Customers.GetAll().Any())
            return;

        EnterCustomerIDLine: Messages.InputMessage("customer ID");
        var customerIdInput = Console.ReadLine();
        int customerId;
        bool isTrueFormat = int.TryParse(customerIdInput, out customerId);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("Customer ID");
            goto EnterCustomerIDLine;
        }

        var customer = _unitOfWork.Customers.Get(customerId);
        if (customer is null)
        {
            Messages.NotFoundMessage("Customer");
            return;
        }

        _unitOfWork.Customers.Delete(customer);
        _unitOfWork.Commit("Customer", "deleted");
    }
    public void DeleteSeller()
    {
        ShowAllSellers();
        if (!_unitOfWork.Sellers.GetAll().Any())
            return;

        EnterSellerIDLine: Messages.InputMessage("seller ID");
        var sellerIDInput = Console.ReadLine();
        int sellerID;
        bool isTrueFormat = int.TryParse(sellerIDInput, out sellerID);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("Seller ID");
            goto EnterSellerIDLine;
        }

        var seller = _unitOfWork.Sellers.Get(sellerID);

        if (seller is null)
        {
            Messages.NotFoundMessage("Seller");
            return;
        }

        _unitOfWork.Sellers.Delete(seller);
        _unitOfWork.Commit("Seller", "deleted");
    }
    public void DeleteCategory()
    {
        if (!_unitOfWork.Categories.GetAll().Any())
        {
            Messages.ThereIsNotMessage("category");
            return;
        }

        foreach (var category in _unitOfWork.Categories.GetAll())
            Console.WriteLine($"Id: {category.Id} Name: {category.Name}");

        EnterIdLine: Messages.InputMessage("id");
        string idInput = Console.ReadLine();
        int id;
        bool isTrueFormat = int.TryParse(idInput, out id);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("id");
            goto EnterIdLine;
        }

        var existCategory = _unitOfWork.Categories.Get(id);

        if (existCategory is null)
        {
            Messages.NotFoundMessage("Category");
            return;
        }

        _unitOfWork.Categories.Delete(existCategory);
        _unitOfWork.Commit("Category", "deleted");
    }
}