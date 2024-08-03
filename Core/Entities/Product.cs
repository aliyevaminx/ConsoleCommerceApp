namespace Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int SellerId { get; set; }
    public Seller Seller { get; set; }
    public ICollection<Order> Orders { get; set; }
}
