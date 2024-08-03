using Core.Entities.Base;

namespace Data.Repository.Base;

public interface IRepository<T> where T : BaseEntity
{

    List<T> GetAll();
    T Get(int id);
    void Add(T item);
    void Update(T item);
    void Delete(T item);

}
