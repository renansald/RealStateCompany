using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyTypeRepository : IBaseRepository<PropertyTypeEntity>
{
    public Task<IEnumerable<PropertyTypeEntity>> GetList();
}