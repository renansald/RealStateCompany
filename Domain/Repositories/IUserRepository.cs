using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<int> Create(string name, string email, string password, string role);
    Task<UserEntity> Authentication(string email, string password);
    Task<bool> IsUserAlreadyRegistered(string email);

}