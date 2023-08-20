using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Repositories;

namespace Domain.Business.City;

public class CityBusiness : ICityBusiness
{
    private readonly ICityRepository _cityRepository;

    public CityBusiness(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<int> Create(CityDTO item)
    {
        var cityEntity = item.ConvertFromDto();

        return await _cityRepository.Create(cityEntity); ;
    }

    public async Task Update(CityDTO item)
    {
        var cityEntity = item.ConvertFromDto();
        await _cityRepository.Update(cityEntity);
    }

    public async Task Delete(int id)
    {
        await _cityRepository.DeleteById(id);
    }

    public async Task<CityDTO> GetById(int id)
    {
        var city = await _cityRepository.GetById(id);
        return city.ConvertToDto();
    }

    public async Task<List<CityDTO>> GetList()
    {
        var citiesEntity = await _cityRepository.GetList();
        return citiesEntity.ConvertToListDto();
    }
}