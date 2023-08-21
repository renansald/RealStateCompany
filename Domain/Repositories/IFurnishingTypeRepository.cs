using Domain.Entities;

namespace Domain.Repositories;

public interface IFurnishingTypeRepository : IBaseRepository<FurnishingTypeEntity>
{
    Task<IEnumerable<FurnishingTypeEntity>> GetList();
}