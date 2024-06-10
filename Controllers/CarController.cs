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

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(new GeneralResponse<IEnumerable<Car>>(true, "Cars retrieved successfully", cars));
        }

        // GET: api/Car/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarById(int id)
        {
            var car = await _carService.GetAsync(c => c.Id == id);
            if (car == null)
            {
                return NotFound(new GeneralResponse<Car>(false, "Car not found", null));
            }

            return Ok(new GeneralResponse<Car>(true, "Car retrieved successfully", car));
        }

        // POST: api/Car
        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar(Car car)
        {
            if (car == null)
            {
                return BadRequest(new GeneralResponse<Car>(false, "Invalid car data", null));
            }

            await _carService.AddAsync(car);
            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, new GeneralResponse<Car>(true, "Car created successfully", car));
        }

        // PUT: api/Car/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, Car car)
        {
            if (id != car.Id || car == null)
            {
                return BadRequest(new GeneralResponse<Car>(false, "Car ID mismatch or invalid data", null));
            }

            var existingCar = await _carService.GetAsync(c => c.Id == id);
            if (existingCar == null)
            {
                return NotFound(new GeneralResponse<Car>(false, "Car not found", null));
            }

            await _carService.UpdateAsync(car);
            return NoContent();
        }

        // DELETE: api/Car/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _carService.GetAsync(c => c.Id == id);
            if (car == null)
            {
                return NotFound(new GeneralResponse<Car>(false, "Car not found", null));
            }

            await _carService.DeleteAsync(id);
            return NoContent();
        }
    }
}
