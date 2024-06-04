using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAgencyController : ControllerBase
    {
        private readonly ICarAgencyService _carAgencyService;

        public CarAgencyController(ICarAgencyService carAgencyService)
        {
            _carAgencyService = carAgencyService;
        }

        // GET: api/CarAgency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAgency>>> GetCarAgencies()
        {
            var carAgencies = await _carAgencyService.GetAllAsync();
            return Ok(new GeneralResponse<IEnumerable<CarAgency>>(true, "Car agencies retrieved successfully", carAgencies));
        }

        // GET: api/CarAgency/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarAgency>> GetCarAgencyById(int id)
        {
            var carAgency = await _carAgencyService.GetAsync(ca => ca.Id == id);
            if (carAgency == null)
            {
                return NotFound(new GeneralResponse<CarAgency>(false, "Car agency not found", null));
            }

            return Ok(new GeneralResponse<CarAgency>(true, "Car agency retrieved successfully", carAgency));
        }

        // POST: api/CarAgency
        [HttpPost]
        public async Task<ActionResult<CarAgency>> CreateCarAgency(CarAgency carAgency)
        {
            if (carAgency == null)
            {
                return BadRequest(new GeneralResponse<CarAgency>(false, "Invalid car agency data", null));
            }

            await _carAgencyService.AddAsync(carAgency);
            return CreatedAtAction(nameof(GetCarAgencyById), new { id = carAgency.Id }, new GeneralResponse<CarAgency>(true, "Car agency created successfully", carAgency));
        }

        // PUT: api/CarAgency/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarAgency(int id, CarAgency carAgency)
        {
            if (id != carAgency.Id || carAgency == null)
            {
                return BadRequest(new GeneralResponse<CarAgency>(false, "Car agency ID mismatch or invalid data", null));
            }

            var existingCarAgency = await _carAgencyService.GetAsync(ca => ca.Id == id);
            if (existingCarAgency == null)
            {
                return NotFound(new GeneralResponse<CarAgency>(false, "Car agency not found", null));
            }

            await _carAgencyService.UpdateAsync(carAgency);
            return NoContent();
        }

        // DELETE: api/CarAgency/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAgency(int id)
        {
            var carAgency = await _carAgencyService.GetAsync(ca => ca.Id == id);
            if (carAgency == null)
            {
                return NotFound(new GeneralResponse<CarAgency>(false, "Car agency not found", null));
            }

            await _carAgencyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
