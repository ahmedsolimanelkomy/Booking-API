using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _RoomTypeService;

        public RoomTypeController(IRoomTypeService RoomTypeService)
        {
            _RoomTypeService = RoomTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<RoomType>>>> GetRoomTypes([FromQuery] string[] includeProperties)
        {
            var response = await _RoomTypeService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<RoomType>>(true, "RoomTypes retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<RoomType>>> GetRoomType(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _RoomTypeService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<RoomType>(false, "RoomType not found", null));
            }
            return Ok(new GeneralResponse<RoomType>(true, "RoomType retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<RoomType>>> PostRoomType(RoomType RoomType)
        {
            await _RoomTypeService.AddAsync(RoomType);
            return CreatedAtAction(nameof(GetRoomType), new { id = RoomType.Id }, new GeneralResponse<RoomType>(true, "RoomType added successfully", RoomType));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<RoomType>>> PutRoomType(int id, RoomType RoomType)
        {
            if (id != RoomType.Id)
            {
                return BadRequest(new GeneralResponse<RoomType>(false, "RoomType ID mismatch", null));
            }

            await _RoomTypeService.UpdateAsync(RoomType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<RoomType>>> DeleteRoomType(int id)
        {
            var existingRoomType = await _RoomTypeService.GetAsync(b => b.Id == id);
            if (existingRoomType == null)
            {
                return NotFound(new GeneralResponse<RoomType>(false, "RoomType not found", null));
            }

            await _RoomTypeService.DeleteAsync(id);
            return Ok(new GeneralResponse<RoomType>(true, "RoomType deleted successfully", existingRoomType));
        }
    }
}

