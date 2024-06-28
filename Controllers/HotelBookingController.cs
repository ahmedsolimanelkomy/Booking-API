using Booking_API.DTOs;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelBookingDTO>>>> GetAllBookings()
        {
            var bookings = await _service.GetAllBookingsAsync();
            if (bookings == null || !bookings.Any())
            {
                return Ok(new GeneralResponse<IEnumerable<HotelBookingDTO>>(false, "No bookings found.", bookings));
            }
            return Ok(new GeneralResponse<IEnumerable<HotelBookingDTO>>(true, "Bookings retrieved successfully.", bookings));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingDTO>>> GetBookingByID(int id)
        {
            var booking = await _service.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound(new GeneralResponse<HotelBookingDTO>(false, "Booking not found.", null));
            }
            return Ok(new GeneralResponse<HotelBookingDTO>(true, "Booking retrieved successfully.", booking));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingDTO>>> UpdateBooking(int id, HotelBookingDTO bookingDTO)
        {
            var updatedBooking = await _service.UpdateBookingAsync(id, bookingDTO);
            if (updatedBooking == null)
            {
                return NotFound(new GeneralResponse<HotelBookingDTO>(false, "Booking not found.", null));
            }
            return Ok(new GeneralResponse<HotelBookingDTO>(true, "Booking updated successfully.", updatedBooking));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<string>>> DeleteBooking(int id)
        {
            await _service.DeleteBookingAsync(id);
            return Ok(new GeneralResponse<string>(true, "Booking deleted successfully.", null));
        }

        [HttpGet("filtered")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<FilteredBookingDTO>>>> GetFilteredHotelsAsync([FromQuery] HotelBookingFilterDTO filter)
        {
            var filteredBookings = await _service.GetFilteredBookingsAsync(filter);

            if (filteredBookings == null || !filteredBookings.Any())
            {
                return Ok(new GeneralResponse<IEnumerable<FilteredBookingDTO>>( false,"No bookings found matching the filter criteria.",
                  filteredBookings ));
            }

            return Ok(new GeneralResponse<IEnumerable<FilteredBookingDTO>>( true,"Bookings retrieved successfully.", filteredBookings));
        }
    }
}
