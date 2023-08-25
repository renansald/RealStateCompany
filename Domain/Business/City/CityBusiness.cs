using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Utils;

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

    public async Task Update(CityDTO item, int id)
    {
        Validations.ValidateUpdate(id, item.Id);
        var cityEntity = item.ConvertFromDto();
        await _cityRepository.Update(cityEntity);
    }

    public async Task Delete(int id)
    {
        Validations.ValidateId(id);

        await _cityRepository.DeleteById(id);
    }

    public async Task<CityDTO> GetById(int id)
    {
        Validations.ValidateId(id);

        var city = await _cityRepository.GetById(id);
        return city.ConvertToDto();
    }

    public async Task<List<CityDTO>> GetList()
    {
        var citiesEntity = await _cityRepository.GetList();
        return citiesEntity.ConvertToListDto();
    }
}