using Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace Domain.Business.Interfaces;

public interface IPhotosBusiness
{
    Task<List<PhotosDTO>> Upload(List<IFormFile> files, int propertyId);

    Task<List<PhotosDTO>> GetPhotos(int propertyId);

    Task Delete(int photoId);

}