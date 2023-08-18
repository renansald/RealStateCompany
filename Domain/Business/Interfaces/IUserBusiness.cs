using Domain.DTOs;

namespace Domain.Business.Interfaces;

public interface IUserBusiness : IBusinessBase<UserDTO>
{
    Task<AuthenticationDTO> Authentication(string name,  string password);
}