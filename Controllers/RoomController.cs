using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _RoomService;

        public RoomController(IRoomService RoomService)
        {
            _RoomService = RoomService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Room>>>> GetRooms([FromQuery] string[] includeProperties)
        {
            var response = await _RoomService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Room>>(true, "Rooms retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Room>>> GetRoom(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _RoomService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Room>(false, "Room not found", null));
            }
            return Ok(new GeneralResponse<Room>(true, "Room retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Room>>> PostRoom(Room Room)
        {
            await _RoomService.AddAsync(Room);
            return CreatedAtAction(nameof(GetRoom), new { id = Room.Id }, new GeneralResponse<Room>(true, "Room added successfully", Room));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Room>>> PutRoom(int id, Room Room)
        {
            if (id != Room.Id)
            {
                return BadRequest(new GeneralResponse<Room>(false, "Room ID mismatch", null));
            }

            await _RoomService.UpdateAsync(Room);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Room>>> DeleteRoom(int id)
        {
            var existingRoom = await _RoomService.GetAsync(b => b.Id == id);
            if (existingRoom == null)
            {
                return NotFound(new GeneralResponse<Room>(false, "Room not found", null));
            }

            await _RoomService.DeleteAsync(id);
            return Ok(new GeneralResponse<Room>(true, "Room deleted successfully", existingRoom));
        }
    }
}

