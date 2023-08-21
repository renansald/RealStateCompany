using Domain.DTOs;
using Domain.Entities;

namespace Domain.Business.FurnishingType;

public static class Mapper
{
    public static FurnishingTypeEntity ConvertFromDto(this FurnishingTypeDTO dto)
    {
        return new FurnishingTypeEntity
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }

    public static FurnishingTypeDTO ConvertToDto(this FurnishingTypeEntity entity)
    {
        return new FurnishingTypeDTO
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public static List<FurnishingTypeDTO> ConvertToListDto(
        this IEnumerable<FurnishingTypeEntity> entityList)
    {
        return entityList.Select(x => new FurnishingTypeDTO
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }
}