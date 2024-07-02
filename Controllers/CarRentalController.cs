using Booking_API.DTOs.CarRental;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalService _carRentalService;

        public CarRentalController(ICarRentalService carRentalService)
        {
            _carRentalService = carRentalService;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredCarRentals([FromQuery] CarRentalFilterationDTO filter)
        {
            var result = await _carRentalService.GetFilteredCarRentalsAsync(filter);
            return Ok(result);
        }
    }
}
