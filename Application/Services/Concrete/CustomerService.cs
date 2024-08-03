using Core.Entities;
using System.Globalization;

namespace Application.Services.Concrete;

public class CustomerService : ICustomerService
{

    private readonly UnitOfWork _unitOfWork;

    public CustomerService()
    {
        _unitOfWork = new UnitOfWork();
    }

    public void ShowPurchasedProducts(Customer customer)
    {
        if (customer.Orders.Count == 0)
        {
            Messages.NotFoundMessage("Products");
            return;
        }

        foreach (var order in customer.Orders)
        {
            var seller = _unitOfWork.Sellers.Get(order.SellerId);

            Console.WriteLine($"Product: {order.Product.Name} Price: {order.Product.Price} Count: {order.productCount} " +
            $"Total: {order.totalAmount} Seller: {seller.Name} {seller.Surname}");
        }
    }
    public void ShowPurchasedProductsByDate(Customer customer)
    {
        if (customer.Orders.Any())
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

        foreach (var order in customer.Orders)
        {
            if (orderDate.ToString("dd.MM.yyyy") == order.OrderTime.ToString("dd.MM.yyyy"))
            {
                existProduct = true;
                var seller = _unitOfWork.Sellers.Get(order.SellerId);
                Console.WriteLine($"Product: {order.Product.Name} Price: {order.Product.Price} Count: {order.productCount} " +
                                  $"Total: {order.totalAmount} Seller: {seller.Name} {seller.Surname}");
            }
        }

        if (!existProduct)
            Messages.NotFoundMessage("Product");
    }
    public void SearchProduct()
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
    public void BuyProduct(Customer customer)
    {
    EnterProductNameLine: Messages.InputMessage("product name to buy");
        string product = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(product))
        {
            Messages.InvalidInputMessage("product name");
            goto EnterProductNameLine;
        }

        var existProduct = _unitOfWork.Products.GetByName(product);

        if (existProduct is null)
        {
            Messages.NotFoundMessage(product);
            return;
        }

        Messages.InputMessage("count");
        string productCountInput = Console.ReadLine();
        int productCount;
        bool isTrueFormat = int.TryParse(productCountInput, out productCount);

        if (!isTrueFormat)
        {
            Messages.InvalidInputMessage("count");
            goto EnterProductNameLine;
        }

        if (productCount > existProduct.Count)
        {
            Messages.NotFoundMessage(product);
            goto EnterProductNameLine;
        }

        var seller = _unitOfWork.Sellers.Get(existProduct.SellerId);
        var totalAmount = existProduct.Price * productCount;

        Console.WriteLine($"Product name: {product} Count: {productCount} Total Amount: {totalAmount} Seller: {seller.Name} {seller.Surname} \n" +
            $"Do you confirm the purchase? (y / n)");

        Messages.InputMessage("choice");
        string choice = Console.ReadLine();

        if (choice.isValidChoice())
        {
            if (choice == "y")
            {
                int newCountOfProduct = existProduct.Count - productCount;
                if (newCountOfProduct != existProduct.Count) { existProduct.Count = newCountOfProduct; }

                Order order = new Order
                {
                    OrderTime = DateTime.Now,
                    productCount = productCount,
                    totalAmount = totalAmount,
                    SellerId = existProduct.SellerId,
                    CustomerId = customer.Id,
                    ProductId = existProduct.Id
                };

                _unitOfWork.Orders.Add(order);

                if (newCountOfProduct == 0)
                    _unitOfWork.Products.Delete(existProduct);
                else
                    _unitOfWork.Products.Update(existProduct);

                _unitOfWork.Commit(product, "purchased");
            }
            else
                return;
        }
    }
}
