using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Business.User;

public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserBusiness(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<int> Create(UserDTO item)
    {
        if (await _userRepository.IsUserAlreadyRegistered(item.Email))
        {
            throw new BadRequestException("User already exists");
        }
        return await _userRepository.Create(name: item.Name, email: item.Email,
            password: item.Password, role: item.Role);
    }

    public Task Update(UserDTO item)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthenticationDTO> Authentication(string email, string password)
    {
        var user = await _userRepository
                       .Authentication(email: email, password: password) 
                        ?? throw new UnauthorizedAccessException(
                            "User not found or password wrong"
                        );
            
        var token = CreateJWT(user);

        return new AuthenticationDTO
        {
            UserName = user.Name,
            Token = token
        };

    }

    private string CreateJWT(UserEntity user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration
                .GetSection("AppSettings:TokenKey").Value));

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var signingCredentials = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = signingCredentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }
}