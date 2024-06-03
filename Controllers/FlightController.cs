using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _FlightService;

        public FlightController(IFlightService FlightService)
        {
            _FlightService = FlightService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Flight>>>> GetFlights([FromQuery] string[] includeProperties)
        {
            var response = await _FlightService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Flight>>(true, "Flights retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Flight>>> GetFlight(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _FlightService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Flight>(false, "Flight not found", null));
            }
            return Ok(new GeneralResponse<Flight>(true, "Flight retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Flight>>> PostFlight(Flight Flight)
        {
            await _FlightService.AddAsync(Flight);
            return CreatedAtAction(nameof(GetFlight), new { id = Flight.Id }, new GeneralResponse<Flight>(true, "Flight added successfully", Flight));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Flight>>> PutFlight(int id, Flight Flight)
        {
            if (id != Flight.Id)
            {
                return BadRequest(new GeneralResponse<Flight>(false, "Flight ID mismatch", null));
            }

            await _FlightService.UpdateAsync(Flight);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Flight>>> DeleteFlight(int id)
        {
            var existingFlight = await _FlightService.GetAsync(b => b.Id == id);
            if (existingFlight == null)
            {
                return NotFound(new GeneralResponse<Flight>(false, "Flight not found", null));
            }

            await _FlightService.DeleteAsync(id);
            return Ok(new GeneralResponse<Flight>(true, "Flight deleted successfully", existingFlight));
        }
    }
}
