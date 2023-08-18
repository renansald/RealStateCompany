using Domain.Business.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class User : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ILogger<User> _logger;

        public User(IUserBusiness userBusiness, ILogger<User> logger)
        {
            _userBusiness = userBusiness;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] UserDTO user)
        {
            try
            {
                var userLogged = await _userBusiness
                    .Authentication(user.Name, user.Password);

                return Ok(userLogged);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return Unauthorized(new {message = ex.Message});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new {message="Error during authentication"});
            }
        }
    }
}
