namespace Data.Repository.Abstract;

public interface IAdminRepository : IRepository<Admin>
{
    Admin GetAdminByEmail(string email);
}
