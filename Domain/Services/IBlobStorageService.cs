using Microsoft.AspNetCore.Http;

namespace Domain.Services;

public interface IBlobStorageService
{
    Task<List<string>> Create(List<IFormFile> files, int propertyId);

}