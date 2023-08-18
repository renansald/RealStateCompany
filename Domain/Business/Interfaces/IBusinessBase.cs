namespace Domain.Business.Interfaces;

public interface IBusinessBase<T>
{
    public Task<int> Create(T item);
    public Task Update(T item);
    public Task Delete(int id);
    public Task<T> GetById(int id);
}