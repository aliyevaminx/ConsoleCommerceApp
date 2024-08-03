namespace Data.Repository.Abstract;

public interface IProductRepository : IRepository<Product>
{
    List<Product> GetProductBySubstring(string substring);
    Product GetByName(string name);
}
