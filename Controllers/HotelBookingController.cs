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
    public class HotelBookingController : ControllerBase
    {
        private readonly IHotelBookingService _service;

        public HotelBookingController(IHotelBookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelBookingDTO>>> GetAllBookings()
        {
            var bookings = await _service.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotelBookingDTO>> GetBookingByID(int id)
        {
            var booking = await _service.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, HotelBookingDTO bookingDTO)
        {
            var updatedBooking = await _service.UpdateBookingAsync(id, bookingDTO);
            if (updatedBooking == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _service.DeleteBookingAsync(id);
            return NoContent();
        }
    }

}
