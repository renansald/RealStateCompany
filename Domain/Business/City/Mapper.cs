using Domain.DTOs;
using Domain.Entities;

namespace Domain.Business.City;

public static class Mapper
{
    public static CityDTO ConvertToDTO(this CityEntity city)
    {
        return new CityDTO
        {
            Id = city.Id,
            Name = city.Name
        };
    }

    public static CityEntity ConvertFromDTO(this CityDTO city)
    {
        return new CityEntity()
        {
            Id = city.Id,
            Name = city.Name
        };
    }
}