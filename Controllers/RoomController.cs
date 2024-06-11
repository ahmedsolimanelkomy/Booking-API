using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _RoomService;
        private readonly IHotelService hotelService;

        public RoomController(IRoomService RoomService, IHotelService hotelService)
        {
            _RoomService = RoomService;
            this.hotelService = hotelService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Room>>>> GetRooms([FromQuery] string[] includeProperties)
        {
            var response = await _RoomService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Room>>(true, "Rooms retrieved successfully", response));
        }

        [HttpGet("GetHotelRooms/{id:int}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Room>>>> GetHotelRooms(int id, [FromQuery] string[] includeProperties)
        {
            Hotel? hotel = await hotelService.GetAsync(h => h.Id == id);
            if (hotel == null)
            {
                return BadRequest(new GeneralResponse<IEnumerable<Room>>(false, "Hotel Not Exist", null));
            }

            var response = await _RoomService.GetListAsync(r => r.HotelId == id, includeProperties);
            if (response.Count() == 0)
            {
                return NotFound(new GeneralResponse<IEnumerable<Room>>(false, "Hotel have no rooms", null));
            }
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
        public async Task<ActionResult<GeneralResponse<Room>>> PostRoom(RoomDTO RoomDTO)
        {
            var Room = await _RoomService.AddDTOAsync(RoomDTO);
            return CreatedAtAction(nameof(GetRoom), new { id = Room.Id }, new GeneralResponse<Room>(true, "Room added successfully", Room));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<Room>>> PatchRoom(int id, RoomDTO RoomDTO)
        {
            if (id != RoomDTO.Id)
            {
                return BadRequest(new GeneralResponse<Room>(false, "Room ID mismatch", null));
            }
            await _RoomService.UpdateDTOAsync(RoomDTO);
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

