using Azure.Storage.Blobs;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Domain.Exceptions;
using Azure.Storage.Blobs.Models;
using Domain.Entities;

namespace Infrastructure.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _blobContainerClient;

    public BlobStorageService(IConfiguration configuration)
    {
        _blobContainerClient = new BlobServiceClient(configuration.GetConnectionString("BlobStorage"))
                .GetBlobContainerClient(configuration.GetSection("BlobContainers:Photos").Value);
        
    }

    public async Task<IEnumerable<PhotosEntity>> Create(List<IFormFile> files, int propertyId)
    {
        
        var areThereFiles = files.Select(x =>
        {
            var blobName = $@"{propertyId}/{x.FileName}";

            var blobClient = _blobContainerClient.GetBlobClient(blobName);

            if(blobClient.Exists())
                return x.FileName;
            return null;
        }).Where(x => x != null);

        if (areThereFiles.Any())
        {
            throw new BadRequestException(
                $@"There are the following files: {string.Join(", ", areThereFiles)}"
            );
        }

        var photosEntity = new List<PhotosEntity>();
        foreach (var file in files)
        {
            var fileName = $@"{propertyId}/{file.FileName}";
            
            using var memoryStream = new MemoryStream();
            
            var blobHttpHeader = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            };
            
            await file.CopyToAsync(memoryStream);
            
            memoryStream.Position = 0;
            
            var blobClient =  _blobContainerClient
                .GetBlobClient(fileName);

            await blobClient.UploadAsync(memoryStream, blobHttpHeader);

            photosEntity.Add(new PhotosEntity
            {
                PropertyId = propertyId,
                Url = blobClient.Uri.AbsoluteUri.ToString(),
            });
        }

        return photosEntity;
    }
}

