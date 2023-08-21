using Domain.DTOs;

namespace Domain.Business.Interfaces;

public interface IFurnishingTypeBusiness : IBusinessBase<FurnishingTypeDTO>
{
    Task<IEnumerable<FurnishingTypeDTO>> GetList();
}