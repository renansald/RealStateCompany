namespace Domain.Business.Interfaces;

public interface IBusinessBase<T>
{
    public int? Create(T item);
    public void Update(T item);
    public void Delete(int id);
    public T GetById(int id);
}