using Domain.DTOs;
using Domain.Entities;

namespace Domain.Business.User;

public static class Mapper
{
    public static UserDTO ConvertToDto(this UserEntity user)
    {
        return new UserDTO()
        {
            Name = user.Name,
            Id = user.Id,
            Password = user.Password
        };
    }
}