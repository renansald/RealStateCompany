using System.Net;
using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace RealStateCompany.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class City : ControllerBase
    {
        private readonly ICityBusiness _cityBusiness;
        private readonly ILogger<City> _logger;

        public City(ICityBusiness cityBusiness, ILogger<City> logger)
        {
            _cityBusiness = cityBusiness;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _cityBusiness.GetById(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                await _cityBusiness.Delete(id);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Error on delete city");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityDTO city)
        {
            try
            {
                var id = await _cityBusiness.Create(city);
                return StatusCode(201, new { Id = id });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Error on create city");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CityDTO city)
        {
            try
            {
                await _cityBusiness.Update(city);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Error on update city");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var cities = await _cityBusiness.GetList();
                return Ok(cities);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}