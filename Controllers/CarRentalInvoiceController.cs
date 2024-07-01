using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalInvoiceController : ControllerBase
    {
        private readonly ICarRentalInvoiceService CarRentalInvoiceService;

        public CarRentalInvoiceController(ICarRentalInvoiceService CarRentalInvoiceService)
        {
            this.CarRentalInvoiceService = CarRentalInvoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CarRentalInvoice>>>> GetCarRentalInvoices([FromQuery] string[] includeProperties)
        {
            var response = await CarRentalInvoiceService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<CarRentalInvoice>>(true, "CarRentalInvoices retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoice>>> GetCarRentalInvoice(int id, [FromQuery] string[] includeProperties)
        {
            var response = await CarRentalInvoiceService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<CarRentalInvoice>(false, "CarRentalInvoice not found", null));
            }
            return Ok(new GeneralResponse<CarRentalInvoice>(true, "CarRentalInvoice retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoice>>> PostCarRentalInvoice(CarRentalInvoice CarRentalInvoice)
        {
            await CarRentalInvoiceService.AddAsync(CarRentalInvoice);
            return CreatedAtAction(nameof(GetCarRentalInvoice), new { id = CarRentalInvoice.Id }, new GeneralResponse<CarRentalInvoice>(true, "CarRentalInvoice added successfully", CarRentalInvoice));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoice>>> PutCarRentalInvoice(int id, CarRentalInvoice CarRentalInvoice)
        {
            if (id != CarRentalInvoice.Id)
            {
                return BadRequest(new GeneralResponse<CarRentalInvoice>(false, "CarRentalInvoice ID mismatch", null));
            }

            await CarRentalInvoiceService.UpdateAsync(CarRentalInvoice);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<CarRentalInvoice>>> DeleteCarRentalInvoice(int id)
        {
            var existingCarRentalInvoice = await CarRentalInvoiceService.GetAsync(b => b.Id == id);
            if (existingCarRentalInvoice == null)
            {
                return NotFound(new GeneralResponse<CarRentalInvoice>(false, "CarRentalInvoice not found", null));
            }

            await CarRentalInvoiceService.DeleteAsync(id);
            return Ok(new GeneralResponse<CarRentalInvoice>(true, "CarRentalInvoice deleted successfully", existingCarRentalInvoice));
        }
    }
}
