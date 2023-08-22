using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Services;

public interface IBlobStorageService
{
    Task<IEnumerable<PhotosEntity>> Create(List<IFormFile> files, int propertyId);
}