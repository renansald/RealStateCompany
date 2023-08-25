using System.Security.Cryptography.X509Certificates;
using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class PropertyType : ControllerBase
{
    private readonly IPropertyTypeBusiness _propertyTypeBusiness;
    private readonly ILogger<PropertyType> _logger;

    public PropertyType(IPropertyTypeBusiness propertyTypeBusiness, 
        ILogger<PropertyType> logger)
    {
        _propertyTypeBusiness = propertyTypeBusiness;
        _logger = logger;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] PropertyTypeDTO propertyType)
    {
        try
        {
            var id = await _propertyTypeBusiness.Create(propertyType);
            return Ok(new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, new
            {
                message = "Internal server error on create property type"
            });
        }
    }

    [HttpDelete("/{id}/delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _propertyTypeBusiness.Delete(id);
            return Ok();
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
                message = "Internal Server Error On Delete Property Type"
            });
        }
    }

    [HttpPut("/{id}/update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] PropertyTypeDTO propertyType)
    {
        try
        {
            await _propertyTypeBusiness.Update(propertyType, id);

            return Ok();
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
                message = ex.Message
            });
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var projectType = await _propertyTypeBusiness.GetById(id);

            return Ok(projectType);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new {message = ex.Message});
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, new
            {
                message = "Internal Server Error On Get Property Type"
            });
        }

    }

    [HttpGet("list")]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var properties = await _propertyTypeBusiness.GetList();
            return Ok(properties);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, new
            {
                message = "Internal Server Error on Get Properties"
            });
        }
    }
}