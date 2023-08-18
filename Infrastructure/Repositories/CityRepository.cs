using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly DataContext _dataContext;

    public CityRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<CityEntity> GetById(int id)
    {
        var city = await _dataContext.Cities.FindAsync(id) 
                ?? throw new NotFoundException("City not found");
        return city;
    }

    public async Task DeleteById(int id)
    {
        try
        {
            var city = await _dataContext.Cities.FindAsync(id)
                       ?? throw new NotFoundException("City doesn't exist on database");
            _dataContext.Cities.Remove(city);
            await _dataContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<int> Create(CityEntity entity)
    {
        try
        {
            await _dataContext.Cities.AddAsync(entity);
            
            await _dataContext.SaveChangesAsync();
            
            return entity.Id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task Update(CityEntity entity)
    {
        try
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<IEnumerable<CityEntity>> GetList()
    {
        try
        {
            return await _dataContext.Cities.ToListAsync() 
                   ?? throw new NotFoundException("Cities not found");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}