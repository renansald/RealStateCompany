using Domain.DTOs;
using Domain.Entities;

namespace Domain.Business.PropertyType;

public static class Mapper
{
    public static PropertyTypeEntity ConvertFromDto(this PropertyTypeDTO dto)
    {
        return new PropertyTypeEntity
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }

    public static PropertyTypeDTO ConvertToDto(this PropertyTypeEntity entity)
    {
        return new PropertyTypeDTO
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public static List<PropertyTypeDTO> ConvertToListDto(
        this IEnumerable<PropertyTypeEntity> entityList)
    {
        return entityList.Select(x => new PropertyTypeDTO
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();
    }
}