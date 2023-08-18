namespace Domain.Repositories;

public interface IBaseRepository<T>
{
    public Task<T> GetById(int id);
    public Task DeleteById(int id);
    public Task<int> Create(T entity);
    public Task Update(T entity);
}