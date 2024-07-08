using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.CarAgencyDTOS;
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
        private readonly IMapper _mapper;

        public CarAgencyController(ICarAgencyService carAgencyService, IMapper mapper)
        {
            _carAgencyService = carAgencyService;
            _mapper = mapper;
        }

        // GET: api/CarAgency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAgencyViewDTO>>> GetCarAgencies()
        {
            var carAgencies = await _carAgencyService.GetAllAsync();
            var carAgencyDTOs = _mapper.Map<IEnumerable<CarAgencyViewDTO>>(carAgencies);
            return Ok(new GeneralResponse<IEnumerable<CarAgencyViewDTO>>(true, "Car agencies retrieved successfully", carAgencyDTOs));
        }

        //// GET: api/CarAgency
        //[HttpGet("GetFilteredCarAgencies")]
        //public async Task<ActionResult<IEnumerable<CarAgencyViewDTO>>> GetFilteredCarAgencies([FromQuery] CarAgencyFilterDTO carAgencyFilterDTO)
        //{
        //    var carAgencyDTOs = await _carAgencyService.GetFilteredCarAgencies(carAgencyFilterDTO);
        //    if(!carAgencyDTOs.Any())
        //    {
        //        return BadRequest(new GeneralResponse<IEnumerable<CarAgencyViewDTO>>(false, "there is no car agency with this filteration", null));
        //    }
        //    return Ok(new GeneralResponse<IEnumerable<CarAgencyViewDTO>>(true, "Car agencies filtered successfully", carAgencyDTOs));
        //}

        // GET: api/CarAgency/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarAgencyViewDTO>> GetCarAgencyById(int id)
        {
            var carAgency = await _carAgencyService.GetAsync(ca => ca.Id == id);
            if (carAgency == null)
            {
                return NotFound(new GeneralResponse<CarAgencyViewDTO>(false, "Car agency not found", null));
            }

            var carAgencyDTO = _mapper.Map<CarAgencyViewDTO>(carAgency);
            return Ok(new GeneralResponse<CarAgencyViewDTO>(true, "Car agency retrieved successfully", carAgencyDTO));
        }

        // POST: api/CarAgency
        [HttpPost]
        public async Task<ActionResult<CarAgencyDTO>> CreateCarAgency([FromForm] CarAgencyDTO carAgencyDTO)
        {
            if (carAgencyDTO == null)
            {
                return BadRequest(new GeneralResponse<CarAgencyDTO>(false, "Invalid car agency data", null));
            }

            var carAgency = _mapper.Map<CarAgency>(carAgencyDTO);
            var PhotoUrl = await _carAgencyService.SavePhoto(carAgencyDTO.AgencyPhoto);
            carAgency.AgencyPhotoURL = PhotoUrl;

            await _carAgencyService.AddAsync(carAgency);
            var createdCarAgencyDTO = _mapper.Map<CarAgencyDTO>(carAgency);
            return CreatedAtAction(nameof(GetCarAgencyById), new { id = createdCarAgencyDTO.Id }, new GeneralResponse<CarAgencyDTO>(true, "Car agency created successfully", createdCarAgencyDTO));
        }

        // PUT: api/CarAgency/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCarAgency(int id, CarAgencyDTO carAgencyDTO)
        {
            if (id != carAgencyDTO.Id || carAgencyDTO == null)
            {
                return BadRequest(new GeneralResponse<CarAgencyDTO>(false, "Car agency ID mismatch or invalid data", null));
            }

            var existingCarAgency = await _carAgencyService.GetAsync(ca => ca.Id == id);
            if (existingCarAgency == null)
            {
                return NotFound(new GeneralResponse<CarAgencyDTO>(false, "Car agency not found", null));
            }

            _mapper.Map(carAgencyDTO, existingCarAgency);
            await _carAgencyService.UpdateAsync(existingCarAgency);
            return CreatedAtAction(nameof(UpdateCarAgency), new { id = existingCarAgency.Id }, new GeneralResponse<CarAgencyDTO>(true, "Car agency created successfully", carAgencyDTO));
        }

        // DELETE: api/CarAgency/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAgency(int id)
        {
            var carAgency = await _carAgencyService.GetAsync(ca => ca.Id == id, ["CarRentals", "Cars", "CarAgencyReviews"]);
            if (carAgency == null)
            {
                return NotFound(new GeneralResponse<CarAgencyDTO>(false, "Car agency not found", null));
            }

            await _carAgencyService.DeleteAsync(id);
            return Ok(new GeneralResponse<CarAgency>(true, "Car agency Deleted successfully", carAgency));
        }
    }
}
