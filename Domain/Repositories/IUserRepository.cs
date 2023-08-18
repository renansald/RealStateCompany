using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{

    Task<UserEntity> Authentication(string user, string password);
    
}