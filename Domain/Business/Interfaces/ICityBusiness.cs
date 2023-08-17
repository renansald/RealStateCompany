using Domain.DTOs;

namespace Domain.Business.Interfaces;

public interface ICityBusiness : IBusinessBase<CityDTO>
{
    List<CityDTO> GetList();
}