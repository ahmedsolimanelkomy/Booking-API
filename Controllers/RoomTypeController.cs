using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.NewFolder;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IMapper _mapper;

        public RoomTypeController(IRoomTypeService roomTypeService, IMapper mapper)
        {
            _roomTypeService = roomTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<RoomTypeDTO>>>> GetRoomTypes([FromQuery] string[] includeProperties)
        {
            try
            {
                var response = await _roomTypeService.GetAllAsync(includeProperties);
                var roomTypes = _mapper.Map<IEnumerable<RoomTypeDTO>>(response);

                return Ok(new GeneralResponse<IEnumerable<RoomTypeDTO>>(true, "RoomTypes retrieved successfully", roomTypes));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<IEnumerable<RoomTypeDTO>>(false, ex.Message, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<RoomTypeDTO>>> GetRoomType(int id, [FromQuery] string[] includeProperties)
        {
            try
            {
                var response = await _roomTypeService.GetAsync(b => b.Id == id, includeProperties);
                if (response == null)
                {
                    return NotFound(new GeneralResponse<RoomTypeDTO>(false, "RoomType not found", null));
                }

                var roomType = _mapper.Map<RoomTypeDTO>(response);
                return Ok(new GeneralResponse<RoomTypeDTO>(true, "RoomType retrieved successfully", roomType));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<RoomTypeDTO>(false, ex.Message, null));
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<RoomTypeDTO>>> PostRoomType(RoomTypeDTO roomTypeDTO)
        {
            try
            {
                var roomType = _mapper.Map<RoomType>(roomTypeDTO);
                await _roomTypeService.AddAsync(roomType);
                var createdRoomTypeDTO = _mapper.Map<RoomTypeDTO>(roomType);

                return CreatedAtAction(nameof(GetRoomType), new { id = createdRoomTypeDTO.Id }, new GeneralResponse<RoomTypeDTO>(true, "RoomType added successfully", createdRoomTypeDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<RoomTypeDTO>(false, ex.Message, null));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<RoomTypeDTO>>> PutRoomType(int id, RoomTypeDTO roomTypeDTO)
        {
            if (id != roomTypeDTO.Id)
            {
                return BadRequest(new GeneralResponse<RoomTypeDTO>(false, "RoomType ID mismatch", null));
            }

            try
            {
                var existingRoomType = await _roomTypeService.GetAsync(f => f.Id == id);
                if (existingRoomType == null)
                {
                    return NotFound(new GeneralResponse<RoomTypeDTO>(false, "RoomType not found", null));
                }

                _mapper.Map(roomTypeDTO, existingRoomType);
                await _roomTypeService.UpdateAsync(existingRoomType);

                return Ok(new GeneralResponse<RoomTypeDTO>(true, "RoomType updated successfully", roomTypeDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<RoomTypeDTO>(false, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<RoomTypeDTO>>> DeleteRoomType(int id)
        {
            try
            {
                var existingRoomType = await _roomTypeService.GetAsync(f => f.Id == id);
                if (existingRoomType == null)
                {
                    return NotFound(new GeneralResponse<RoomTypeDTO>(false, "RoomType not found", null));
                }

                await _roomTypeService.DeleteAsync(id);
                return Ok(new GeneralResponse<RoomTypeDTO>(true, "RoomType deleted successfully", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<RoomTypeDTO>(false, ex.Message, null));
            }
        }
    }
}
