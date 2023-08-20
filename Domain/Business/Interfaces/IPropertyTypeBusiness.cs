using Domain.DTOs;

namespace Domain.Business.Interfaces;

public interface IPropertyTypeBusiness : IBusinessBase<PropertyTypeDTO>
{
    Task<List<PropertyTypeDTO>> GetList();
}