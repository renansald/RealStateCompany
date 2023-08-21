using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize(Roles = "Admin, User")]
public class FurnishingType : ControllerBase
{
    private readonly IFurnishingTypeBusiness _furnishingTypeBusiness;
    private readonly ILogger<FurnishingType> _logger;

    public FurnishingType(IFurnishingTypeBusiness furnishingTypeBusiness,
        ILogger<FurnishingType> logger)
    {
        _furnishingTypeBusiness = furnishingTypeBusiness;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid Id");
            }

            var furnishingType = await _furnishingTypeBusiness.GetById(id);

            return Ok(furnishingType);
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
                message = $@"Internal Server Error on Get Furnishing Type"
            });
        }
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetList()
    {
        try
        {
            var response = _furnishingTypeBusiness.GetList();
            return Ok(response);
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
                message = $@"Internal Server Error on get furnishing type list"
            });
        }
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] FurnishingTypeDTO furnishingType)
    {
        try
        {
            var id = await _furnishingTypeBusiness.Create(furnishingType);
            return Ok(new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, new
            {
                message = "Internal Server Error on Create Furnishing Type"
            });
        }
    }

    [HttpDelete("{id}/delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (id <= 0)
            {
                throw new BadRequestException("Invalid Id");
            }

            await _furnishingTypeBusiness.Delete(id);

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
                message = "Internal Server Error on Delete Furnishing Type"
            });
        }
    }

    [HttpPut("{id}/update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] FurnishingTypeDTO item)
    {
        try
        {
            if (id <= 0 || id != item.Id) throw new BadRequestException("Invalid Id");

            await _furnishingTypeBusiness.Update(item);

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
                message = $@"Internal Server Error on Update Furnishing Type"
            });
        }
    }
}