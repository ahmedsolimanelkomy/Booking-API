using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.RoomDTOS;
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
        private readonly IMapper mapper;

        public RoomController(IRoomService RoomService, IHotelService hotelService, IMapper mapper)
        {
            _RoomService = RoomService;
            this.hotelService = hotelService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Room>>>> GetRooms([FromQuery] string[] includeProperties)
        {
            var response = await _RoomService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Room>>(true, "Rooms retrieved successfully", response));
        }

        [HttpGet("GetHotelRooms/{id:int}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<RoomViewDTO>>>> GetHotelRooms(int id)
        {
            Hotel? hotel = await hotelService.GetAsync(h => h.Id == id);

            if (hotel == null)
            {
                return BadRequest(new GeneralResponse<IEnumerable<RoomViewDTO>>(false, "Hotel Not Exist", null));
            }

            var response = await _RoomService.GetListAsync(r => r.HotelId == id, ["RoomType","Hotel"]);

            IList<RoomViewDTO> Rooms = new List<RoomViewDTO>();

            mapper.Map(response, Rooms);

            if (response.Count() == 0)
            {
                return NotFound(new GeneralResponse<IEnumerable<RoomViewDTO>>(false, "Hotel have no rooms", null));
            }

            return Ok(new GeneralResponse<IEnumerable<RoomViewDTO>>(true, "Rooms retrieved successfully", Rooms));
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
        public async Task<ActionResult<GeneralResponse<Room>>> PostRoom(RoomViewDTO RoomDTO)
        {
            var Room = await _RoomService.AddDTOAsync(RoomDTO);
            return CreatedAtAction(nameof(GetRoom), new { id = Room.Id }, new GeneralResponse<Room>(true, "Room added successfully", Room));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<Room>>> PatchRoom(int id, RoomViewDTO RoomDTO)
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

