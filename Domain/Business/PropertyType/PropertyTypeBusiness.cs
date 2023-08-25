using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Utils;

namespace Domain.Business.PropertyType;

public class PropertyTypeBusiness : IPropertyTypeBusiness
{
    private readonly IPropertyTypeRepository _propertyTypeRepository;

    public PropertyTypeBusiness(IPropertyTypeRepository propertyTypeRepository)
    {
        _propertyTypeRepository = propertyTypeRepository;
    }

    public async Task<int> Create(PropertyTypeDTO item)
    {
        var propertyTypeEntity = item.ConvertFromDto();

        return await _propertyTypeRepository.Create(propertyTypeEntity);
    }

    public async Task Update(PropertyTypeDTO item, int id)
    {
        Validations.ValidateUpdate(id, item.Id);

        var propertyTypeEntity = item.ConvertFromDto();

        await _propertyTypeRepository.Update(propertyTypeEntity);
    }

    public async Task Delete(int id)
    {
        Validations.ValidateId(id);
        await _propertyTypeRepository.DeleteById(id);
    }

    public async Task<PropertyTypeDTO> GetById(int id)
    {
        Validations.ValidateId(id);
        var propertyTypeEntity = await _propertyTypeRepository.GetById(id);
        return propertyTypeEntity.ConvertToDto();
    }

    public async Task<List<PropertyTypeDTO>> GetList()
    {
        var entityList = await _propertyTypeRepository.GetList();
        return entityList.ConvertToListDto();
    }
}