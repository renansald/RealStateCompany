using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Exceptions;
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
        public async Task<IActionResult> Authentication([FromBody] LoginDTO user)
        {
            try
            {
                var userLogged = await _userBusiness
                    .Authentication(user.Email, user.Password);

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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            try
            {
                var userId = await _userBusiness.Create(user);
                return StatusCode(201, new { Id = userId });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}
