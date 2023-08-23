using Domain.Entities;

namespace Domain.Repositories;

public interface IPhotosRepository : IBaseRepository<PhotosEntity>
{
    void AddList(ref IEnumerable<PhotosEntity> photos);

    Task<IEnumerable<PhotosEntity>> GetListByPropertyId (int propertyId);
    Task SetPrimary(int id);
}