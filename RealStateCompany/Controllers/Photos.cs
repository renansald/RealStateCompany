using Domain.Business.Interfaces;
using Domain.Exceptions;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize(Roles = "Admin, User")]
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

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _photosBusiness.Delete(id);
            
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
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
                message = "Internal Server Error on Delete Photo"
            });
        }
    }

    [HttpGet("{propertyId}/list")]
    public async Task<IActionResult> GetPhotos(int propertyId)
    {
        try
        {
            var photos = _photosBusiness.GetPhotos(propertyId);
            return Ok(photos);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, new
            {
                message = "Internal Server Error on Get Photos"
            });
        }
    }

    [HttpPatch("{id}/primary")]
    public async Task<IActionResult> SetPrimary(int id)
    {
        try
        {
            await _photosBusiness.SetPrimary(id);
            return Ok();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, new
            {
                message = "Internal Server Error on Update Primary Photo"
            });
        }
    }
}