using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using Domain.Utils;
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
        Validations.ValidateId(propertyId);
        //TODO: Add method to validate if property exists
        var photosEntity = await _blobStorageService.Create(files, propertyId);
        _photosRepository.AddList(ref photosEntity);
        var photosDto = photosEntity.ConvertToDtoList();
        return photosDto;
    }

    public async Task<List<PhotosDTO>> GetPhotos(int propertyId)
    {
        Validations.ValidateId(propertyId);
        //TODO: Add method to validate if property exists
        var photosEntity = await _photosRepository.GetListByPropertyId(propertyId);
        var photosDto = photosEntity.ConvertToDtoList();
        return photosDto;
    }

    public async Task Delete(int photoId)
    {
        Validations.ValidateId(photoId);
        var photo = await _photosRepository.GetById(photoId);
        await _blobStorageService.Delete(photo);
        await _photosRepository.DeleteById(photoId);
    }

    public async Task SetPrimary(int id)
    {
        Validations.ValidateId(id);
        await _photosRepository.SetPrimary(id);
    }
}