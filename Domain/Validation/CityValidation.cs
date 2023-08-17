using Domain.DTOs;
using Domain.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Validation;

public static class CityValidation
{
    public static void IdValidation(this int id)
    {
        if(id <= 0) throw new BadRequestException("Invalid city id");
    }

    public static void CreateCityValidation(this CityDTO city)
    {
        var errors = new List<string>(2);

        if (string.IsNullOrEmpty(city.Name)) errors.Add("City must have a name");
        if(city.Id != 0) errors.Add("Invalid parameter Id");

        if (errors.Count > 0)
        {
            throw new BadRequestException(string.Join('\n', errors));
        }
    }

    public static void UpdateCityValidation(this CityDTO city)
    {
        var errors = new List<string>(2);
        if (string.IsNullOrEmpty(city.Name)) errors.Add("City must have a name");
        if (city.Id <= 0) errors.Add("Not exists city with this Id");

        if (errors.Count > 0)
        {
            throw new BadRequestException(string.Join('\n', errors));
        }
    }
}