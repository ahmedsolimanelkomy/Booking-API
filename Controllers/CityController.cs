using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IService<City> _cityService;

        public CityController(IService<City> cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<City>>>> GetCities([FromQuery] string[] includeProperties=null)
        {
            var response = await _cityService.GetAllAsync(includeProperties);
         
            return Ok(new GeneralResponse<IEnumerable<City>>(true, "Cities retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<City>>> GetCity(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _cityService.GetAsync(c => c.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<City>(false, "City not found", null));
            }
            return Ok(new GeneralResponse<City>(true, "City retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<City>>> PostCity(City city)
        {
            await _cityService.AddAsync(city);
            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, new GeneralResponse<City>(true, "City added successfully", city));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<City>>> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest(new GeneralResponse<City>(false, "City ID mismatch", null));
            }

            await _cityService.UpdateAsync(city);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<City>>> DeleteCity(int id)
        {
            var existingCity = await _cityService.GetAsync(c => c.Id == id);
            if (existingCity == null)
            {
                return NotFound(new GeneralResponse<City>(false, "City not found", null));
            }

            await _cityService.DeleteAsync(id);
            return Ok(new GeneralResponse<City>(true, "City deleted successfully", existingCity));
        }
    }
}
