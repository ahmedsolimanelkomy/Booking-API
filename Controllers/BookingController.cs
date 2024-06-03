using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services;
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
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Booking>>>> GetBookings([FromQuery] string[] includeProperties)
        {
            var response = await _bookingService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Booking>>(true, "Bookings retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Booking>>> GetBooking(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _bookingService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Booking>(false, "Booking not found", null));
            }
            return Ok(new GeneralResponse<Booking>(true, "Booking retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Booking>>> PostBooking(Booking booking)
        {
            await _bookingService.AddAsync(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, new GeneralResponse<Booking>(true, "Booking added successfully", booking));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Booking>>> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest(new GeneralResponse<Booking>(false, "Booking ID mismatch", null));
            }

            await _bookingService.UpdateAsync(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Booking>>> DeleteBooking(int id)
        {
            var existingBooking = await _bookingService.GetAsync(b => b.Id == id);
            if (existingBooking == null)
            {
                return NotFound(new GeneralResponse<Booking>(false, "Booking not found", null));
            }

            await _bookingService.DeleteAsync(id);
            return Ok(new GeneralResponse<Booking>(true, "Booking deleted successfully", existingBooking));
        }
    }
}
