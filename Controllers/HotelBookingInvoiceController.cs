using AutoMapper;
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
        private readonly IMapper mapper;

        public HotelBookingInvoiceController(IHotelBookingInvoiceService HotelBookingInvoiceService, IMapper mapper)
        {
            this.HotelBookingInvoiceService = HotelBookingInvoiceService;
            this.mapper = mapper;
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
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoiceDTO>>> PostHotelBookingInvoice(HotelBookingInvoiceDTO hotelBookingInvoiceDTO)
        {
            var hotelBookingInvoice = mapper.Map<HotelBookingInvoice>(hotelBookingInvoiceDTO);
            await HotelBookingInvoiceService.AddAsync(hotelBookingInvoice);
            var responseDTO = mapper.Map<HotelBookingInvoiceDTO>(hotelBookingInvoice);
            return CreatedAtAction(nameof(GetHotelBookingInvoice), new { id = hotelBookingInvoice.Id }, new GeneralResponse<HotelBookingInvoiceDTO>(true, "HotelBookingInvoice added successfully", responseDTO));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoiceDTO>>> PutHotelBookingInvoice(int id, HotelBookingInvoiceDTO hotelBookingInvoiceDTO)
        {
            if (id != hotelBookingInvoiceDTO.Id)
            {
                return BadRequest(new GeneralResponse<HotelBookingInvoiceDTO>(false, "HotelBookingInvoice ID mismatch", null));
            }

            var hotelBookingInvoice = mapper.Map<HotelBookingInvoice>(hotelBookingInvoiceDTO);
            await HotelBookingInvoiceService.UpdateAsync(hotelBookingInvoice);
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
