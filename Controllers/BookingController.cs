using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelBooking>>>> GetBookings([FromQuery] string[] includeProperties)
        {
            var response = await _bookingService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<HotelBooking>>(true, "Bookings retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBooking>>> GetBooking(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _bookingService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<HotelBooking>(false, "Booking not found", null));
            }
            return Ok(new GeneralResponse<HotelBooking>(true, "Booking retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<HotelBooking>>> PostBooking(HotelBooking booking)
        {
            await _bookingService.AddAsync(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, new GeneralResponse<HotelBooking>(true, "Booking added successfully", booking));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBooking>>> PutBooking(int id, HotelBooking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest(new GeneralResponse<HotelBooking>(false, "Booking ID mismatch", null));
            }

            await _bookingService.UpdateAsync(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBooking>>> DeleteBooking(int id)
        {
            var existingBooking = await _bookingService.GetAsync(b => b.Id == id);
            if (existingBooking == null)
            {
                return NotFound(new GeneralResponse<HotelBooking>(false, "Booking not found", null));
            }

            await _bookingService.DeleteAsync(id);
            return Ok(new GeneralResponse<HotelBooking>(true, "Booking deleted successfully", existingBooking));
        }
    }
}
