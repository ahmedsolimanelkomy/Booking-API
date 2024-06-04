using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _HotelService;

        public HotelController(IHotelService HotelService)
        {
            _HotelService = HotelService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Hotel>>>> GetHotels([FromQuery] string[] includeProperties)
        {
            var response = await _HotelService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Hotel>>(true, "Hotels retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Hotel>>> GetHotel(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _HotelService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Hotel>(false, "Hotel not found", null));
            }
            return Ok(new GeneralResponse<Hotel>(true, "Hotel retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Hotel>>> PostHotel(Hotel Hotel)
        {
            await _HotelService.AddAsync(Hotel);
            return CreatedAtAction(nameof(GetHotel), new { id = Hotel.Id }, new GeneralResponse<Hotel>(true, "Hotel added successfully", Hotel));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Hotel>>> PutHotel(int id, Hotel Hotel)
        {
            if (id != Hotel.Id)
            {
                return BadRequest(new GeneralResponse<Hotel>(false, "Hotel ID mismatch", null));
            }

            await _HotelService.UpdateAsync(Hotel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Hotel>>> DeleteHotel(int id)
        {
            var existingHotel = await _HotelService.GetAsync(b => b.Id == id);
            if (existingHotel == null)
            {
                return NotFound(new GeneralResponse<Hotel>(false, "Hotel not found", null));
            }

            await _HotelService.DeleteAsync(id);
            return Ok(new GeneralResponse<Hotel>(true, "Hotel deleted successfully", existingHotel));
        }
    }
}

