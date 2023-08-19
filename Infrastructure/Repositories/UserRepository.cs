using System.Security.Cryptography;
using Domain.Entities;
using Domain.Exceptions;
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

    public async Task<int> Create(string name, string email, string password, string role)
    {
        var passwordDict = CreatePasswordKeyAndHash(password);

        var user = new UserEntity
        {
            Email = email,
            Password = passwordDict["passwordHash"],
            PasswordKey = passwordDict["passwordKey"],
            Name = name,
            Role = role
        };

        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();
        return user.Id;
        
    }

    public async Task<UserEntity> Authentication(string email, string password)
    {
        var user = await _dataContext.Users.FirstOrDefaultAsync(
            x => x.Email == email);

        if (user is null || !IsPasswordMatch(password, 
                user.Password, user.PasswordKey))
        {
            return null;
        }

        return user;
    }

    private bool IsPasswordMatch(string password, byte[] passwordHash, byte[] passwordKey)
    {
        using var hmac = new HMACSHA512(passwordKey);
        
        var passwordEncrypted = hmac.ComputeHash(
            System.Text.Encoding.UTF8.GetBytes(password));
        return passwordHash.SequenceEqual(passwordEncrypted);
    }

    public async Task<bool> IsUserAlreadyRegistered(string email)
    {
        return await _dataContext.Users.AnyAsync(x => x.Email.Equals(email));
    }

    public async Task<bool> IsUserAlreadyRegistered(int id)
    {
        return await _dataContext.Users.AnyAsync(x => x.Id.Equals(id));
    }

    public async Task Update(UserEntity user, string password)
    {
        var passwordConfig = CreatePasswordKeyAndHash(password);

        user.Password = passwordConfig["passwordHash"];
        user.PasswordKey = passwordConfig["passwordKey"];

        _dataContext.Entry(user).State = EntityState.Modified;
        await _dataContext.SaveChangesAsync();
    }

    public async Task Delete(int id, string password)
    {
        if (!(await IsUserAlreadyRegistered(id)))
        {
            throw new BadRequestException("This user doesn't exist");
        }

        var user = _dataContext.Users.FirstOrDefault(x => x.Id.Equals(id));

        if (!IsPasswordMatch(password, user.Password, user.PasswordKey))
        {
            throw new BadRequestException("User doesn't match");
        }

        _dataContext.Users.Remove(user);
        await _dataContext.SaveChangesAsync();
    }

    private Dictionary<string, byte[]> CreatePasswordKeyAndHash(string password)
    {
        var hash = new Dictionary<string, byte[]>();
        
        using var hmac = new HMACSHA512();
        hash.Add("passwordKey", hmac.Key);
        hash.Add("passwordHash", hmac.ComputeHash(
            System.Text.Encoding.UTF8.GetBytes(password)));

        return hash;
    }
}