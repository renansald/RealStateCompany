using Domain.Business.Interfaces;
using Domain.Exceptions;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
//[Authorize(Roles = "Admin, User")]
public class Photos : ControllerBase
{

    private readonly IPhotosBusiness _photosBusiness;
    private readonly ILogger<Photos> _logger;

    public Photos(IPhotosBusiness photosBusiness, ILogger<Photos> logger)
    {
        _photosBusiness = photosBusiness;
        _logger = logger;
    }

    [HttpPost("{id}/upload")]
    public async Task<IActionResult> Upload(List<IFormFile> files, int id)
    {
        try
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid Id");
            }

            var items = await _photosBusiness.Upload(files, id);

            return Ok(items);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, new
            {
                message = "Internal Server Error on Upload Photos"
            });
        }
    }
}