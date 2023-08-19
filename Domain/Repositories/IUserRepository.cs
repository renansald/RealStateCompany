using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<int> Create(string name, string email, string password, string role);
    Task<UserEntity> Authentication(string email, string password);
    Task<bool> IsUserAlreadyRegistered(string email);
    Task<bool> IsUserAlreadyRegistered(int id);
    Task Update(UserEntity user, string password);
    Task Delete(int id, string password);
}