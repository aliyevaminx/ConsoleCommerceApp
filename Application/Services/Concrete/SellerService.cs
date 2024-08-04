using Core.Entities;
using System.Globalization;

namespace Application.Services.Concrete;

public class SellerService : ISellerService
{
    private readonly UnitOfWork _unitOfWork;

    public SellerService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void ShowSoldProducts(Seller seller)
    {
        var orders = _unitOfWork.Orders.GetAllOrdersBySeller(seller.Id);

        if (_unitOfWork.Orders.GetCountOfOrders(orders) <= 0)
        {
            Messages.NotFoundMessage("Products");
            return;
        }

        foreach (var order in orders)
        {
            var customer = _unitOfWork.Customers.Get(order.CustomerId);
            var product = order.Product;

            Console.WriteLine($"Product: {product.Name} Price: {product.Price} Count: {order.productCount} " +
                $"Total: {order.totalAmount} Customer: {customer.Name} {customer.Surname}");
        }
    }
    public void ShowSoldProductsByDate(Seller seller)
    {
        var orders = _unitOfWork.Orders.GetAllOrdersBySeller(seller.Id);

        if (_unitOfWork.Orders.GetCountOfOrders(orders) <= 0)
        {
            Messages.NotFoundMessage("Products");
            return;
        }

    EnterOrderDateLine: Messages.InputMessage("date (dd.MM.yyyy)");
        string orderDateInput = Console.ReadLine();
        DateTime orderDate;
        bool isTrueFormat = DateTime.TryParseExact(orderDateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out orderDate);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("Order date");
            goto EnterOrderDateLine;
        }

        bool existProduct = false;

        foreach (var order in orders)
        {
            if (orderDate.ToString("dd.MM.yyyy") == order.OrderTime.ToString("dd.MM.yyyy"))
            {
                existProduct = true;
                var customer = _unitOfWork.Customers.Get(order.CustomerId);
                Console.WriteLine($"Product: {order.Product.Name} Price: {order.Product.Price} Count: {order.productCount} " +
                                  $"Total: {order.totalAmount} Customer: {customer.Name} {customer.Surname}");
            }
        }

        if (!existProduct)
            Messages.NotFoundMessage("Product");
    }
    public void ShowIncome(Seller seller)
    {
        var orders = _unitOfWork.Orders.GetAllOrdersBySeller(seller.Id);
        decimal totalIncome = 0;

        foreach (var order in orders)
            totalIncome += order.totalAmount;

        Console.WriteLine($"Total Income: {totalIncome}");
    }
    public void SearchProduct(Seller seller)
    {
    EnterProductNameLine: Messages.InputMessage("product name");
        string productName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(productName))
        {
            Messages.InvalidInputMessage("product name");
            goto EnterProductNameLine;
        }

        var existProductList = _unitOfWork.Products.GetProductBySubstring(productName);

        if (existProductList is null)
        {
            Messages.NotFoundMessage("Product");
            goto EnterProductNameLine;
        }

        foreach (var product in existProductList)
            Console.WriteLine($"Name {product.Name} Price: {product.Price} Count: {product.Count}");
    }
    public void AddProduct(Seller seller)
    {
    EnterProductNameLine: Messages.InputMessage("product name");
        string productName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(productName))
        {
            Messages.InvalidInputMessage("Product name");
            goto EnterProductNameLine;
        }
    EnterProductPriceLine: Messages.InputMessage("price");
        string priceInput = Console.ReadLine();
        decimal price;
        bool isTrueFormat = decimal.TryParse(priceInput, out price);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("price");
            goto EnterProductPriceLine;
        }

    EnterProductCountLine: Messages.InputMessage("count");
        string countInput = Console.ReadLine();
        int count;
        isTrueFormat = int.TryParse(countInput, out count);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("Count");
            goto EnterProductCountLine;
        }

        foreach (var category in _unitOfWork.Categories.GetAll())
        {
            Console.WriteLine($"Id: {category.Id} Name: {category.Name}");
        }

    EnterCategeryIdLine: Messages.InputMessage("category id");
        string categeryIdInput = Console.ReadLine();
        int categeryId;
        isTrueFormat = int.TryParse(categeryIdInput, out categeryId);
        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("category id");
            goto EnterCategeryIdLine;
        }

        var existedCategory = _unitOfWork.Categories.Get(categeryId);

        if (existedCategory is null)
        {
            Messages.NotFoundMessage("Category");
            return;
        }


        Product product = new Product
        {
            Name = productName,
            Price = price,
            Count = count,
            CategoryId = categeryId,
            SellerId = seller.Id
        };

        _unitOfWork.Products.Add(product);
        _unitOfWork.Commit(productName, "added");
    }
    public void ChangeCountOfProduct(Seller seller)
    {
        var products = _unitOfWork.Products.GetAllProductsBySeller(seller.Id);

        foreach (var product in products)
            Console.WriteLine($"Id: {product.Id} Name: {product.Name} Count: {product.Count}");

        EnterProductIdLine: Messages.InputMessage("product ID");
        string productIdInput = Console.ReadLine(); 
        int productId;
        bool isTrueFormat = int.TryParse(productIdInput, out productId);

        var existProduct = _unitOfWork.Products.Get(productId);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("Product ID");
            goto EnterProductIdLine;
        }

        if (existProduct is null)
        {
            Messages.NotFoundMessage("Product");
            return;
        }

        int newCount = existProduct.Count;
    EnterNewCountLine: Messages.InputMessage("new count");
        string newCountInput = Console.ReadLine();
        isTrueFormat = int.TryParse(newCountInput, out newCount);
        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("New count");
            goto EnterNewCountLine;
        }

        if (newCount != existProduct.Count) { existProduct.Count = newCount; }

        _unitOfWork.Products.Update(existProduct);
        _unitOfWork.Commit("Count", "updated");
    }
    public void DeleteProduct(Seller seller)
    {
        var products = _unitOfWork.Products.GetAllProductsBySeller(seller.Id);

        foreach (var product in products)
            Console.WriteLine($"Id: {product.Id} Name: {product.Name} Count: {product.Count}");

        EnterProductIdLine: Messages.InputMessage("product ID");
        string productIdInput = Console.ReadLine();
        int productId;
        bool isTrueFormat = int.TryParse(productIdInput, out productId);

        var existProduct = _unitOfWork.Products.Get(productId);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("product ID");
            goto EnterProductIdLine;
        }

        if (existProduct is null)
        {
            Messages.NotFoundMessage("Product");
            return;
        }

        _unitOfWork.Products.Delete(existProduct);
        _unitOfWork.Commit("Product", "deleted");
    }

}
