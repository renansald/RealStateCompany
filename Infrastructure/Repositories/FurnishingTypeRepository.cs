using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FurnishingTypeRepository : IFurnishingTypeRepository
{
    private readonly DataContext _dataContext;

    public FurnishingTypeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<FurnishingTypeEntity> GetById(int id)
    {
        return await _dataContext.FurnishingType.FindAsync(id) ??
               throw new NotFoundException("Furnishing type doesn't found");
    }

    public async Task DeleteById(int id)
    {
        var furnishingType = await _dataContext.FurnishingType.FindAsync(id)
                             ?? throw new NotFoundException("There isn't furnishing type with this id");

        _dataContext.FurnishingType.Remove(furnishingType);

        await _dataContext.SaveChangesAsync();
    }

    public async Task<int> Create(FurnishingTypeEntity entity)
    {
        await _dataContext.FurnishingType.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Update(FurnishingTypeEntity entity)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;
        await _dataContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<FurnishingTypeEntity>> GetList()
    {
        return await _dataContext.FurnishingType.ToListAsync() ??
               throw new NotFoundException("There aren't any value to furnishing type");
    }
}