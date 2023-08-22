using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Http;

namespace Domain.Business.Photos;

public class PhotosBusiness : IPhotosBusiness
{
    private readonly IPhotosRepository _photosRepository;
    private readonly IBlobStorageService _blobStorageService;

    public PhotosBusiness(IPhotosRepository photosRepository, IBlobStorageService blobStorageService)
    {
        _photosRepository = photosRepository;
        _blobStorageService = blobStorageService;
    }

    public async Task<List<PhotosDTO>> Upload(List<IFormFile> files, int propertyId)
    {
        var photosEntity = await _blobStorageService.Create(files, propertyId);
        _photosRepository.AddList(ref photosEntity);
        var photosDto = photosEntity.ConvertToDtoList();
        return photosDto;
    }

    public async Task<List<PhotosDTO>> GetPhotos(int propertyId)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int photoId)
    {
        throw new NotImplementedException();
    }
}