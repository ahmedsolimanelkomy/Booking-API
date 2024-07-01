using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingInvoiceController : ControllerBase
    {
        private readonly IHotelBookingInvoiceService HotelBookingInvoiceService;

        public HotelBookingInvoiceController(IHotelBookingInvoiceService HotelBookingInvoiceService)
        {
            this.HotelBookingInvoiceService = HotelBookingInvoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelBookingInvoice>>>> GetHotelBookingInvoices([FromQuery] string[] includeProperties)
        {
            var response = await HotelBookingInvoiceService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<HotelBookingInvoice>>(true, "HotelBookingInvoices retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoice>>> GetHotelBookingInvoice(int id, [FromQuery] string[] includeProperties)
        {
            var response = await HotelBookingInvoiceService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<HotelBookingInvoice>(false, "HotelBookingInvoice not found", null));
            }
            return Ok(new GeneralResponse<HotelBookingInvoice>(true, "HotelBookingInvoice retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoice>>> PostHotelBookingInvoice(HotelBookingInvoice HotelBookingInvoice)
        {
            await HotelBookingInvoiceService.AddAsync(HotelBookingInvoice);
            return CreatedAtAction(nameof(GetHotelBookingInvoice), new { id = HotelBookingInvoice.Id }, new GeneralResponse<HotelBookingInvoice>(true, "HotelBookingInvoice added successfully", HotelBookingInvoice));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoice>>> PutHotelBookingInvoice(int id, HotelBookingInvoice HotelBookingInvoice)
        {
            if (id != HotelBookingInvoice.Id)
            {
                return BadRequest(new GeneralResponse<HotelBookingInvoice>(false, "HotelBookingInvoice ID mismatch", null));
            }

            await HotelBookingInvoiceService.UpdateAsync(HotelBookingInvoice);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoice>>> DeleteHotelBookingInvoice(int id)
        {
            var existingHotelBookingInvoice = await HotelBookingInvoiceService.GetAsync(b => b.Id == id);
            if (existingHotelBookingInvoice == null)
            {
                return NotFound(new GeneralResponse<HotelBookingInvoice>(false, "HotelBookingInvoice not found", null));
            }

            await HotelBookingInvoiceService.DeleteAsync(id);
            return Ok(new GeneralResponse<HotelBookingInvoice>(true, "HotelBookingInvoice deleted successfully", existingHotelBookingInvoice));
        }
    }
}
