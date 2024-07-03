using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.CarRental;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalService _carRentalService;
        private readonly IMapper _mapper;

        public CarRentalController(ICarRentalService carRentalService, IMapper mapper)
        {
            _carRentalService = carRentalService;
            _mapper = mapper;
        }


        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredCarRentals([FromQuery] CarRentalFilterationDTO filter)
        {
            var result = await _carRentalService.GetFilteredCarRentalsAsync(filter);
            return Ok(result);
        }

        // GET: api/CarRental
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarRentalDTO>>> GetCarRentals()
        {
            var carRentals = await _carRentalService.GetAllAsync();
            var carRentalDTOs = _mapper.Map<IEnumerable<CarRentalDTO>>(carRentals);
            return Ok(new GeneralResponse<IEnumerable<CarRentalDTO>>(true, "Car rentals retrieved successfully", carRentalDTOs));
        }

        // GET: api/CarRental/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarRentalDTO>> GetCarRentalById(int id)
        {
            var carRental = await _carRentalService.GetAsync(cr => cr.Id == id);
            if (carRental == null)
            {
                return NotFound(new GeneralResponse<CarRentalDTO>(false, "Car rental not found", null));
            }

            var carRentalDTO = _mapper.Map<CarRentalDTO>(carRental);
            return Ok(new GeneralResponse<CarRentalDTO>(true, "Car rental retrieved successfully", carRentalDTO));
        }

        // POST: api/CarRental
        [HttpPost]
        public async Task<ActionResult<CarRentalDTO>> CreateCarRental(CarRentalDTO carRentalDTO)
        {
            if (carRentalDTO == null)
            {
                return BadRequest(new GeneralResponse<CarRentalDTO>(false, "Invalid car rental data", null));
            }

            var carRental = _mapper.Map<CarRental>(carRentalDTO);
            await _carRentalService.AddAsync(carRental);
            var createdCarRentalDTO = _mapper.Map<CarRentalDTO>(carRental);
            return CreatedAtAction(nameof(GetCarRentalById), new { id = createdCarRentalDTO.Id }, new GeneralResponse<CarRentalDTO>(true, "Car rental created successfully", createdCarRentalDTO));
        }

        // PUT: api/CarRental/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCarRental(int id, CarRentalDTO carRentalDTO)
        {
            if (id != carRentalDTO.Id || carRentalDTO == null)
            {
                return BadRequest(new GeneralResponse<CarRentalDTO>(false, "Car rental ID mismatch or invalid data", null));
            }

            var existingCarRental = await _carRentalService.GetAsync(cr => cr.Id == id);
            if (existingCarRental == null)
            {
                return NotFound(new GeneralResponse<CarRentalDTO>(false, "Car rental not found", null));
            }

            _mapper.Map(carRentalDTO, existingCarRental);
            await _carRentalService.UpdateAsync(existingCarRental);
            return CreatedAtAction(nameof(UpdateCarRental), new { id = existingCarRental.Id }, new GeneralResponse<CarRentalDTO>(true, "Car rental created successfully", carRentalDTO));
        }

        // DELETE: api/CarRental/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarRental(int id)
        {
            var carRental = await _carRentalService.GetAsync(cr => cr.Id == id);
            if (carRental == null)
            {
                return NotFound(new GeneralResponse<CarRentalDTO>(false, "Car rental not found", null));
            }

            await _carRentalService.DeleteAsync(id);
            return CreatedAtAction(nameof(DeleteCarRental), new { id = carRental.Id }, new GeneralResponse<CarRentalDTO>(true, "Car rental created successfully", null));
        }
    }
}
