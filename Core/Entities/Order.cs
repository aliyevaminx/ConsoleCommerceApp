namespace Core.Entities;

public class Order : BaseEntity
{
    public DateTime OrderTime { get; set; }
    public int productCount { get; set; }
    public decimal totalAmount { get; set; }
    public int SellerId { get; set; }
    public Seller Seller { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}
