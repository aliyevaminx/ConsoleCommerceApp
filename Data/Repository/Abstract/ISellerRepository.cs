namespace Data.Repository.Abstract;

public interface ISellerRepository : IRepository<Seller>
{
    Seller GetSellerByEmail(string email);
}
