using Domain.DTOs;

namespace Domain.Business.Interfaces;

public interface IUserBusiness
{
    Task<int> Create(UserDTO item);
    Task Update(UserDTO item, int id);
    Task Delete(int id, string password);
    Task<AuthenticationDTO> Authentication(string email,  string password);
}