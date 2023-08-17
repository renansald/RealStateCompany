using Domain.Entities;

namespace Domain.Repositories;

public interface ICityRepository : IBaseRepository<CityEntity>
{
    public IEnumerable<CityEntity> GetList();
}