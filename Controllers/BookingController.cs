using Booking_API.Models;
using Booking_API.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<Booking>>> GetBookings([FromQuery] string[] includeProperties)
    {
        var bookings = await _bookingService.GetAllAsync(includeProperties);
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetBooking(int id, [FromQuery] string[] includeProperties)
    {
        var booking = await _bookingService.GetAsync(b => b.Id == id);
        if (booking == null)
        {
            return NotFound();
        }
        return Ok(booking);
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> PostBooking(Booking booking)
    {
        await _bookingService.AddAsync(booking);
        return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBooking(int id, Booking booking)
    {
        if (id != booking.Id)
        {
            return BadRequest();
        }

        await _bookingService.UpdateAsync(booking);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        await _bookingService.DeleteAsync(id);
        return NoContent();
    }
}
