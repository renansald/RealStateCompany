using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<UserEntity> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Create(UserEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task Update(UserEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> Authentication(string user, string password)
    {
        return await _dataContext.Users.FirstOrDefaultAsync(
            x => x.Name == user && x.Password == password);
    }
}