using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PhotosRepository : IPhotosRepository
{
    private readonly DataContext _dataContext;

    public PhotosRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<PhotosEntity> GetById(int id)
    {
        return await _dataContext.Photos.FindAsync(id)
               ?? throw new NotFoundException("This photo doesn't exist");
    }

    public async Task DeleteById(int id)
    {
        var photo = await _dataContext.Photos.FindAsync(id)
                    ?? throw new NotFoundException("This photo doesn't exist");
        _dataContext.Photos.Remove(photo);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<int> Create(PhotosEntity entity)
    {
        await _dataContext.Photos.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Update(PhotosEntity entity)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;
        await _dataContext.SaveChangesAsync();
    }

    public void AddList(ref IEnumerable<PhotosEntity> photos)
    {
        _dataContext.Photos.AddRange(photos);
        _dataContext.SaveChanges();
    }

    public async Task<IEnumerable<PhotosEntity>> GetListByPropertyId(int propertyId)
    {
        var photos = await _dataContext.Photos.Where(
            x => x.PropertyId.Equals(propertyId)).ToListAsync();

        return photos;
    }
}