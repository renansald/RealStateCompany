namespace Domain.Repositories;

public interface IBaseRepository<T>
{
    public T? GetById(int id);
    public void DeleteById(int id);
    public int? Create(T entity);
    public void Update(T entity);
}