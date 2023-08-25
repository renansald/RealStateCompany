using Domain.Entities;
using Domain.Exceptions;
using Moq;

namespace Tests.Unit.City;

public class CityBusinessTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async void CityBusiness_Delete_Success(int cityId)
    {
        var mock = CityMocks.CityRepositoryMock(citiesEntity:null, cityEntity:null);
        
        var cityBusiness = new Domain.Business.City.CityBusiness( mock.Object);

        await cityBusiness.Delete(cityId);

        mock.Verify(x => x.DeleteById(cityId), Times.Once);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(0)]
    public async void CityBusiness_Delete_BadRequestException(int cityId)
    {
        var mock = CityMocks.CityRepositoryMock(citiesEntity: null, cityEntity: null);

        var cityBusiness = new Domain.Business.City.CityBusiness(mock.Object);

        var exception = await Assert.ThrowsAsync<BadRequestException>(
            () => cityBusiness.Delete(cityId));

        Assert.Equal("Invalid Id", exception.Message);
    }
}