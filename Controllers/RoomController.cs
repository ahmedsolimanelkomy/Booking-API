using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("/api/GetHotelRooms/{id:int}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<RoomViewDTO>>>> GetHotelRooms(int id, [FromQuery] string[] includeProperties)
        {
            Hotel? hotel = await hotelService.GetAsync(h => h.Id == id);

            if (hotel == null)
            {
                return BadRequest(new GeneralResponse<IEnumerable<RoomViewDTO>>(false, "Hotel Not Exist", null));
            }

            var response = await _RoomService.GetListAsync(r => r.HotelId == id, includeProperties);

            IList<RoomViewDTO> Rooms = new List<RoomViewDTO>();

            mapper.Map(response, Rooms);

            if (response.Count() == 0)
            {
                return NotFound(new GeneralResponse<IEnumerable<RoomViewDTO>>(false, "Hotel have no rooms", null));
            }

            return Ok(new GeneralResponse<IEnumerable<RoomViewDTO>>(true, "Rooms retrieved successfully", Rooms));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<AddRoomDTO>>> GetRoom(int id, [FromQuery] string[] includeProperties)
        {
            Room response = await _RoomService.GetAsync(b => b.Id == id, includeProperties);
            AddRoomDTO addRoomDTO = new AddRoomDTO();
            mapper.Map(response, addRoomDTO);
            if (response == null)
            {
                return NotFound(new GeneralResponse<AddRoomDTO>(false, "Room not found", null));
            }
            return Ok(new GeneralResponse<AddRoomDTO>(true, "Room retrieved successfully", addRoomDTO));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Room>>> PostRoom(AddRoomDTO RoomDTO)
        {
            var Room = await _RoomService.AddDTOAsync(RoomDTO);
            return CreatedAtAction(nameof(GetRoom), new { id = Room.Id }, new GeneralResponse<Room>(true, "Room added successfully", Room));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<AddRoomDTO>>> PatchRoom(int id, AddRoomDTO RoomDTO)
        {
            //if (id != RoomDTO.Id)
            //{
            //    return BadRequest(new GeneralResponse<AddRoomDTO>(false, "Room ID mismatch", null));
            //}
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


        #region HotelFilteration
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredHotels([FromQuery] HotelFilterDTO filter)
        {
            var rooms = await _RoomService.GetFilteredRoomsAsync(filter);
            return Ok(rooms);
        }
        #endregion
    }
}

