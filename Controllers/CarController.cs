using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.CarDTOS;
using Booking_API.DTOs.CarRental;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        private readonly ICarAgencyService _carAgencyService;

        public CarController(ICarService carService, IMapper mapper, ICarAgencyService carAgencyService)
        {
            _carService = carService;
            _mapper = mapper;
            _carAgencyService = carAgencyService;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
            var cars = await _carService.GetAllAsync();
            var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(cars);
            return Ok(new GeneralResponse<IEnumerable<CarDTO>>(true, "Cars retrieved successfully", carDTOs));
        }
        
        
        [HttpGet("GetCarByBrand")]
        public async Task<ActionResult<IEnumerable<FilteredCarDTO>>> GetCarByBrand([FromQuery] string Brand)
        {
            IEnumerable<FilteredCarDTO> Cars = await _carService.GetCarByBrand(Brand);
            if (Cars == null || !Cars.Any())
            {
                return Ok(new GeneralResponse<IEnumerable<FilteredCarDTO>>(false, "No cars available in this brand", null));
            }
            return Ok(new GeneralResponse<IEnumerable<FilteredCarDTO>>(true, "cars retrieved successfully", Cars));
        } 
        
        // GET: api/Filteration
        [HttpGet("GetFilteredCars")]
        public async Task<ActionResult<IEnumerable<FilteredCarDTO>>> GetFilteredCars([FromQuery] CarRentalFilterationDTO filter)
        {
            var cars = await _carService.GetFilteredCars(filter);
            if (cars == null || !cars.Any())
            {
                return Ok(new GeneralResponse<IEnumerable<FilteredCarDTO>>(false, "No cars found with the specified criteria", null));
            }
            return Ok(new GeneralResponse<IEnumerable<FilteredCarDTO>>(true, "Cars retrieved successfully", cars));
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
            var car = await _carService.GetAsync(c => c.Id == id, ["CarRentals", "CarReviews", "CarPhotos"]);
            if (car == null)
            {
                return NotFound(new GeneralResponse<CarDTO>(false, "Car not found", null));
            }

            await _carService.DeleteAsync(id);
            return CreatedAtAction(nameof(DeleteCar), new { id = car.Id }, new GeneralResponse<CarDTO>(true, "Car created successfully", null));
        }


        [HttpGet("/api/GetAgencyAndCars/{agencyId}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CarDTO>>>> GetAgencyAndCars(int agencyId, [FromQuery] string[] includeProperties)
        {
            // Check if agency exists
            CarAgency agency = await _carAgencyService.GetAsync(a => a.Id == agencyId);

            if (agency == null)
            {
                return BadRequest(new GeneralResponse<IEnumerable<CarDTO>>(false, "Agency Not Found", null));
            }

            // Retrieve cars for the specified agency
            var carsResponse = await _carService.GetListAsync(c => c.AgencyId == agencyId, includeProperties);

            IList<CarDTO> carsDTO = new List<CarDTO>();

            // Check if carsResponse is a list of Car entities
            if (carsResponse != null && carsResponse.Any())
            {
                // Mapping the response to DTOs
                carsDTO = (IList<CarDTO>)_mapper.Map<IEnumerable<Car>, IEnumerable<CarDTO>>(carsResponse);

                return Ok(new GeneralResponse<IEnumerable<CarDTO>>(true, "Cars retrieved successfully", carsDTO));
            }
            else
            {
                return NotFound(new GeneralResponse<IEnumerable<CarDTO>>(false, "No cars found for this agency", null));
            }
        }


    }
}
