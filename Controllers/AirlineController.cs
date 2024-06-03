using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Airline>>>> GetAirlines([FromQuery] string[] includeProperties)
        {
            var response = await _airlineService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Airline>>(true, "Airlines retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Airline>>> GetAirline(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _airlineService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Airline>(false, "Airline not found", null));
            }
            return Ok(new GeneralResponse<Airline>(true, "Airline retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Airline>>> PostAirline(Airline airline)
        {
            await _airlineService.AddAsync(airline);
            return CreatedAtAction(nameof(GetAirline), new { id = airline.Id }, new GeneralResponse<Airline>(true, "Airline added successfully", airline));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Airline>>> PutAirline(int id, Airline airline)
        {
            if (id != airline.Id)
            {
                return BadRequest(new GeneralResponse<Airline>(false, "Airline ID mismatch", null));
            }

            await _airlineService.UpdateAsync(airline);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Airline>>> DeleteAirline(int id)
        {
            var existingAirline = await _airlineService.GetAsync(b => b.Id == id);
            if (existingAirline == null)
            {
                return NotFound(new GeneralResponse<Airline>(false, "Airline not found", null));
            }

            await _airlineService.DeleteAsync(id);
            return Ok(new GeneralResponse<Airline>(true, "Airline deleted successfully", existingAirline));
        }
    }
}
