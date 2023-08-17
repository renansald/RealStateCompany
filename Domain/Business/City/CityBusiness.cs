using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Validation;

namespace Domain.Business.City;

public class CityBusiness : IBusinessBase<CityDTO>
{
    private readonly ICityRepository _cityRepository;

    public CityBusiness(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public int? Create(CityDTO item)
    {
        try
        {
            item.CreateCityValidation();

            var cityEntity = item.ConvertFromDTO();
            
            return _cityRepository.Create(cityEntity); ;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Update(CityDTO item)
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

    public void Delete(int id)
    {
        try
        {
            id.IdValidation();
            _cityRepository.DeleteById(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public CityDTO GetById(int id)
    {
        try
        {
            id.IdValidation();
            var city = _cityRepository.GetById(id);
            return city.ConvertToDTO();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}