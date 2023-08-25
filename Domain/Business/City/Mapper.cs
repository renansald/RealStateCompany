using Domain.DTOs;
using Domain.Entities;
using System.Linq;

namespace Domain.Business.City;

public static class Mapper
{
    public static CityDTO ConvertToDto(this CityEntity city)
    {
        return new CityDTO
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country
        };
    }

    public static CityEntity ConvertFromDto(this CityDTO city)
    {
        return new CityEntity()
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country
        };
    }

    public static List<CityDTO> ConvertToListDto(this IEnumerable<CityEntity> cities)
    {
        return cities.Select(x => x.ConvertToDto()).ToList();
    }
}