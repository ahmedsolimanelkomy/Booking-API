using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IService<Country> _countryService;
        public CountryController(IService<Country> countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Country>>>> GetCountries([FromQuery] string[] includeProperties=null)
        {
            var response = await _countryService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Country>>(true, "Countries retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Country>>> GetCountry(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _countryService.GetAsync(c => c.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Country>(false, "Country not found", null));
            }
            return Ok(new GeneralResponse<Country>(true, "Country retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Country>>> PostCountry(Country country)
        {
            await _countryService.AddAsync(country);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, new GeneralResponse<Country>(true, "Country added successfully", country));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Country>>> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest(new GeneralResponse<Country>(false, "Country ID mismatch", null));
            }

            await _countryService.UpdateAsync(country);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Country>>> DeleteCountry(int id)
        {
            var existingCountry = await _countryService.GetAsync(c => c.Id == id);
            if (existingCountry == null)
            {
                return NotFound(new GeneralResponse<Country>(false, "Country not found", null));
            }

            await _countryService.DeleteAsync(id);
            return Ok(new GeneralResponse<Country>(true, "Country deleted successfully", existingCountry));
        }
    }
}