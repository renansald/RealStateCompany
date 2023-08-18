using Domain.Entities;

namespace Domain.Repositories;

public interface ICityRepository : IBaseRepository<CityEntity>
{
    public Task<IEnumerable<CityEntity>> GetList();
}