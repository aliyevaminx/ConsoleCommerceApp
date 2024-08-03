namespace Data.Repository.Abstract;

public interface ICategoryRepository : IRepository<Category>
{
    Category GetByName(string name);
}
