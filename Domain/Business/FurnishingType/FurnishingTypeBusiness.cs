using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Repositories;

namespace Domain.Business.FurnishingType;

public class FurnishingTypeBusiness : IFurnishingTypeBusiness
{
    private readonly IFurnishingTypeRepository _furnishingTypeRepository;

    public FurnishingTypeBusiness(IFurnishingTypeRepository furnishingTypeRepository)
    {
        _furnishingTypeRepository = furnishingTypeRepository;
    }

    public async Task<int> Create(FurnishingTypeDTO item)
    {
        var furnishingTypeEntity = item.ConvertFromDto();
        return await _furnishingTypeRepository.Create(furnishingTypeEntity);
    }

    public async Task Update(FurnishingTypeDTO item)
    {
        var furnishingTypeEntity = item.ConvertFromDto();
        await _furnishingTypeRepository.Update(furnishingTypeEntity);
    }

    public async Task Delete(int id)
    {
        await _furnishingTypeRepository.DeleteById(id);
    }

    public async Task<FurnishingTypeDTO> GetById(int id)
    {
        var furnishingTypeEntity = await _furnishingTypeRepository.GetById(id);
        return furnishingTypeEntity.ConvertToDto();
    }

    public async Task<IEnumerable<FurnishingTypeDTO>> GetList()
    {
        var furnishingTypeEntityList = await _furnishingTypeRepository.GetList();
        return furnishingTypeEntityList.ConvertToListDto();
    }
}