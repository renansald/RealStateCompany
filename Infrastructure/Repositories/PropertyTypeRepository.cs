using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PropertyTypeRepository : IPropertyTypeRepository
{
    private readonly DataContext _dataContext;

    public PropertyTypeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<PropertyTypeEntity> GetById(int id)
    {
        return await _dataContext.PropertyType.FindAsync(id)
               ?? throw new NotFoundException("There isn't property type with this Id");
    }

    public async Task DeleteById(int id)
    {
        var propertyType = await _dataContext.PropertyType.FindAsync(id) 
                           ?? throw new BadRequestException("Property type doesn't exist");

        _dataContext.PropertyType.Remove(propertyType);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<int> Create(PropertyTypeEntity entity)
    {
        await _dataContext.PropertyType.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Update(PropertyTypeEntity entity)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;
        await _dataContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<PropertyTypeEntity>> GetList()
    {
        return await _dataContext.PropertyType.ToListAsync();
    }
}