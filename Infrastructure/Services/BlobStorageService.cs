using Azure.Storage;
using Azure.Storage.Blobs;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Infrastructure.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _blobClient;

    public BlobStorageService(IConfiguration configuration)
    {
        var sextion = configuration.GetSection("BlobContainers:Photos").Value;

        _blobClient = new BlobServiceClient(configuration.GetConnectionString("BlobStorage"))
                .GetBlobContainerClient(configuration.GetSection("BlobContainers:Photos").Value);
    }

    public async Task<List<string>> Create(List<IFormFile> files, int propertyId)
    {
        var urlList = new List<string>();
        foreach (var file in files)
        {
            var fileName = $@"{propertyId}/{file.FileName}";
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await _blobClient.UploadBlobAsync(fileName, memoryStream, default);
            urlList.Add(_blobClient.Uri.ToString());
        }

        return urlList;
    }
}

