using System.Net;
using Domain.Business.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace RealStateCompany.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class City : ControllerBase
    {
        private readonly IBusinessBase<CityDTO> _cityBusiness;
        private readonly ILogger<City> _logger;

        public City(IBusinessBase<CityDTO> cityBusiness, ILogger<City> logger)
        {
            _cityBusiness = cityBusiness;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id parameter");
                }
                return Ok(_cityBusiness.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                _cityBusiness.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Error on delete city");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CityDTO city)
        {
            try
            {
                var id = _cityBusiness.Create(city);
                return StatusCode(201, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Error on create city");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] CityDTO city)
        {
            try
            {
                _cityBusiness.Update(city);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, "Error on update city");
            }
        }
    }
}