using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
            var cars = await _carService.GetAllAsync();
            var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(cars);
            return Ok(new GeneralResponse<IEnumerable<CarDTO>>(true, "Cars retrieved successfully", carDTOs));
        }

        // GET: api/Car/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDTO>> GetCarById(int id)
        {
            var car = await _carService.GetAsync(c => c.Id == id);
            if (car == null)
            {
                return NotFound(new GeneralResponse<CarDTO>(false, "Car not found", null));
            }

            var carDTO = _mapper.Map<CarDTO>(car);
            return Ok(new GeneralResponse<CarDTO>(true, "Car retrieved successfully", carDTO));
        }

        // POST: api/Car
        [HttpPost]
        public async Task<ActionResult<CarDTO>> CreateCar(CarDTO carDTO)
        {
            if (carDTO == null)
            {
                return BadRequest(new GeneralResponse<CarDTO>(false, "Invalid car data", null));
            }

            var car = _mapper.Map<Car>(carDTO);
            await _carService.AddAsync(car);
            carDTO = _mapper.Map<CarDTO>(car);
            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, new GeneralResponse<CarDTO>(true, "Car created successfully", carDTO));
        }

        // PUT: api/Car/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarDTO carDTO)
        {
            if (id != carDTO.Id)
            {
                return BadRequest(new GeneralResponse<CarDTO>(false, "Car ID mismatch or invalid data", null));
            }

            var existingCar = await _carService.GetAsync(c => c.Id == id);
            if (existingCar == null)
            {
                return NotFound(new GeneralResponse<CarDTO>(false, "Car not found", null));
            }

            _mapper.Map(carDTO, existingCar); // Map updated fields to the existing car instance
            await _carService.UpdateAsync(existingCar);

            return CreatedAtAction(nameof(UpdateCar), new { id = existingCar.Id }, new GeneralResponse<CarDTO>(true, "Car created successfully", carDTO));
        }


        // DELETE: api/Car/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _carService.GetAsync(c => c.Id == id);
            if (car == null)
            {
                return NotFound(new GeneralResponse<CarDTO>(false, "Car not found", null));
            }

            await _carService.DeleteAsync(id);
            return CreatedAtAction(nameof(DeleteCar), new { id = car.Id }, new GeneralResponse<CarDTO>(true, "Car created successfully", null));
        }
    }
}
