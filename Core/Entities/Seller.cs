namespace Core.Entities;

public class Seller : Person
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string SerialNumber { get; set; }
    public byte[] Salt { get; set; }
    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }
}
