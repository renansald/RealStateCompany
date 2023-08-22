using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Business.Photos;

public static class Mapper
{
    public static List<PhotosDTO> ConvertToDtoList(this IEnumerable<PhotosEntity> entityList)
    {
        return entityList.Select(x => new PhotosDTO
        {
            Url = x.Url,
            PropertyId = x.PropertyId,
            IsPrimary = x.IsPrimary,
            Id = x.Id
        }).ToList();
    }
}