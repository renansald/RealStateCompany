﻿using System.Security.Cryptography;
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

    public async Task<int> Create(string name, string email, string password, string role)
    {
        try
        {

            using var hmac = new HMACSHA512();
            var passwordKey = hmac.Key;
            var passwordHash = hmac.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(password));

            var user = new UserEntity
            {
                Email = email,
                Password = passwordHash,
                PasswordKey = passwordKey,
                Name = name,
                Role = role
            };

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user.Id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
        return await _dataContext.Users.AnyAsync(x => x.Email == email);
    }
}