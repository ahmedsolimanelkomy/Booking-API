using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelBookingInvoice;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingInvoiceController : ControllerBase
    {
        private readonly IHotelBookingInvoiceService _hotelBookingInvoiceService;

        public HotelBookingInvoiceController(IHotelBookingInvoiceService hotelBookingInvoiceService)
        {
            _hotelBookingInvoiceService = hotelBookingInvoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelBookingInvoiceDTO>>>> GetAll()
        {
            var invoiceDTOs = await _hotelBookingInvoiceService.GetAllInvoicesAsync();
            return Ok(new GeneralResponse<IEnumerable<HotelBookingInvoiceDTO>>(true, "Retrieved successfully", invoiceDTOs));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoiceDTO>>> Get(int id)
        {
            var invoiceDTO = await _hotelBookingInvoiceService.GetInvoiceByIdAsync(id);
            if (invoiceDTO == null)
                return NotFound(new GeneralResponse<HotelBookingInvoiceDTO>(false, "Invoice not found", null));

            return Ok(new GeneralResponse<HotelBookingInvoiceDTO>(true, "Retrieved successfully", invoiceDTO));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoiceDTO>>> Create([FromBody] HotelBookingInvoiceDTO invoiceDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new GeneralResponse<HotelBookingInvoiceDTO>(false, "Invalid data", null));

            var createdInvoiceDTO = await _hotelBookingInvoiceService.CreateInvoiceAsync(invoiceDTO);
            return CreatedAtAction(nameof(Get), new { id = createdInvoiceDTO.Id }, new GeneralResponse<HotelBookingInvoiceDTO>(true, "Created successfully", createdInvoiceDTO));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoiceDTO>>> Update(int id, [FromBody] HotelBookingInvoiceDTO invoiceDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new GeneralResponse<HotelBookingInvoiceDTO>(false, "Invalid data", null));

            var updatedInvoiceDTO = await _hotelBookingInvoiceService.UpdateInvoiceAsync(id, invoiceDTO);
            if (updatedInvoiceDTO == null)
                return NotFound(new GeneralResponse<HotelBookingInvoiceDTO>(false, "Invoice not found", null));

            return Ok(new GeneralResponse<HotelBookingInvoiceDTO>(true, "Updated successfully", updatedInvoiceDTO));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingInvoiceDTO>>> Delete(int id)
        {
            var isDeleted = await _hotelBookingInvoiceService.DeleteInvoiceAsync(id);
            if (!isDeleted)
                return NotFound(new GeneralResponse<HotelBookingInvoiceDTO>(false, "Invoice not found", null));

            return Ok(new GeneralResponse<HotelBookingInvoiceDTO>(true, "Deleted successfully", null));
        }
    }
}
