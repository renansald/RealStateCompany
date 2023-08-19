using System.Net;
using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealStateCompany.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles="Admin, User")]
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
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new {message = "Internal server error"});
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                await _cityBusiness.Delete(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CityDTO city)
        {
            try
            {
                var id = await _cityBusiness.Create(city);
                return StatusCode(201, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] CityDTO city)
        {
            try
            {
                await _cityBusiness.Update(city);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new { message = "Internal server error" });
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
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}