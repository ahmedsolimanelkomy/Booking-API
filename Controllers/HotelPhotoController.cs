using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelPhotoController : ControllerBase
    {
        private readonly IHotelPhotoService _HotelPhotoService;

        public HotelPhotoController(IHotelPhotoService HotelPhotoService)
        {
            _HotelPhotoService = HotelPhotoService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelPhoto>>>> GetHotelPhotos([FromQuery] string[] includeProperties)
        {
            var response = await _HotelPhotoService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<HotelPhoto>>(true, "HotelPhotos retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelPhoto>>> GetHotelPhoto(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _HotelPhotoService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<HotelPhoto>(false, "HotelPhoto not found", null));
            }
            return Ok(new GeneralResponse<HotelPhoto>(true, "HotelPhoto retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<HotelPhoto>>> PostHotelPhoto(HotelPhoto HotelPhoto)
        {
            await _HotelPhotoService.AddAsync(HotelPhoto);
            return CreatedAtAction(nameof(GetHotelPhoto), new { id = HotelPhoto.Id }, new GeneralResponse<HotelPhoto>(true, "HotelPhoto added successfully", HotelPhoto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelPhoto>>> PutHotelPhoto(int id, HotelPhoto HotelPhoto)
        {
            if (id != HotelPhoto.Id)
            {
                return BadRequest(new GeneralResponse<HotelPhoto>(false, "HotelPhoto ID mismatch", null));
            }

            await _HotelPhotoService.UpdateAsync(HotelPhoto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelPhoto>>> DeleteHotelPhoto(int id)
        {
            var existingHotelPhoto = await _HotelPhotoService.GetAsync(b => b.Id == id);
            if (existingHotelPhoto == null)
            {
                return NotFound(new GeneralResponse<HotelPhoto>(false, "HotelPhoto not found", null));
            }

            await _HotelPhotoService.DeleteAsync(id);
            return Ok(new GeneralResponse<HotelPhoto>(true, "HotelPhoto deleted successfully", existingHotelPhoto));
        }
    }
}

