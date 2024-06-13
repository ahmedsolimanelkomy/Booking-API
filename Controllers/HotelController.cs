using AutoMapper;
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
        public async Task<ActionResult<GeneralResponse<Hotel>>> PostHotel(HotelDTO hotelDTO)
        {
            var Hotel = await _HotelService.AddDTOAsync(hotelDTO);
            return CreatedAtAction(nameof(GetHotel), new { id = Hotel.Id }, new GeneralResponse<Hotel>(true, "Hotel added successfully", Hotel));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<Hotel>>> PatchHotel(int id, HotelDTO hotelDTO)
        {
            if (id != hotelDTO.Id)
            {
                return BadRequest(new GeneralResponse<Hotel>(false, "Hotel ID mismatch", null));
            }
            await _HotelService.UpdateDTOAsync(hotelDTO);
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

