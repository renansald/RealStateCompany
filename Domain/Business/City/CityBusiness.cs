using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Validation;

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
        try
        {
            item.CreateCityValidation();

            var cityEntity = item.ConvertFromDTO();
            
            return await _cityRepository.Create(cityEntity); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task Update(CityDTO item)
    {
        try
        {
            item.UpdateCityValidation();
            var cityEntity = item.ConvertFromDTO();
            _cityRepository.Update(cityEntity);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            id.IdValidation();
            await _cityRepository.DeleteById(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<CityDTO> GetById(int id)
    {
        try
        {
            id.IdValidation();
            var city = await _cityRepository.GetById(id);
            return city.ConvertToDTO();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public async Task<List<CityDTO>> GetList()
    {
        try
        {
            var citiesEntity = await _cityRepository.GetList();
            return citiesEntity.ConvertToListDto();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}