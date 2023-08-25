using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Tests.Utils;

namespace Tests.Unit.City;

public static class CityMocks
{
    public static Mock<ICityRepository> CityRepositoryMock(IEnumerable<CityEntity>? citiesEntity,
        CityEntity? cityEntity)
    {
        var mock = new Mock<ICityRepository>();

        mock.Setup(x => x.GetList())
            .ReturnsAsync(citiesEntity).Verifiable();
        mock.Setup(x => x.Create(It.IsAny<CityEntity>()))
            .ReturnsAsync(1).Verifiable();
        mock.Setup(x => x.DeleteById(It.IsAny<int>())).Verifiable();
        mock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(cityEntity)
            .Verifiable();
        mock.Setup(x => x.Update(It.IsAny<CityEntity>())).Verifiable();

        return mock;
    }

    public static CityDTO CreateDto(string name, string country)
    {
        return new CityDTO
        {
            Country = country,
            Name = name
        };
    }

    public static CityEntity CreateEntity(string name, string country)
    {
        return new CityEntity
        {
            Name = name,
            Country = country
        };
    }

    public static IEnumerable<CityEntity> CreateEntityList(IEnumerable<CityDTO> cities)
    {
        return cities.Select(x => new CityEntity
        {
            Country = x.Country,
            Name = x.Name,
            Id = x.Id
        });
    }

    public static IEnumerable<CityDTO> CreateDtoList(int quantity)
    {
        var cities = new List<CityDTO>();

        for (var count = 0; count < quantity; count++)
        {
            cities.Add(new CityDTO
            {
                Country = RandomValues.GenerateRandomString(10),
                Name = RandomValues.GenerateRandomString(10),
                Id = count + 1
            });
        }

        return cities;
    }

}