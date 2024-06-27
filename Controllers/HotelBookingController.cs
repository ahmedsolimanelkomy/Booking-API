using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelBookingDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelBookingController : ControllerBase
    {
        private readonly IHotelBookingService _bookingService;
        private readonly BraintreeService _braintreeService;
        private readonly IMapper _mapper;

        public HotelBookingController(IHotelBookingService bookingService, BraintreeService braintreeService, IMapper mapper)
        {
            _bookingService = bookingService;
            _braintreeService = braintreeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelBookingViewDTO>>>> GetHotelBookings([FromQuery] string[] includeProperties)
        {
            var bookings = await _bookingService.GetAllAsync(includeProperties);
            var bookingDTOs = _mapper.Map<IEnumerable<HotelBookingViewDTO>>(bookings);
            return Ok(new GeneralResponse<IEnumerable<HotelBookingViewDTO>>(true, "Bookings retrieved successfully", bookingDTOs));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBookingViewDTO>>> GetHotelBooking(int id, [FromQuery] string[] includeProperties)
        {
            var booking = await _bookingService.GetAsync(b => b.Id == id, includeProperties);
            if (booking == null)
            {
                return NotFound(new GeneralResponse<HotelBooking>(false, "Booking not found", null));
            }

            var bookingDTO = _mapper.Map<HotelBookingViewDTO>(booking);
            return Ok(new GeneralResponse<HotelBookingViewDTO>(true, "Booking retrieved successfully", bookingDTO));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<HotelBookingDto>>> PostHotelBooking([FromBody] HotelBookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<HotelBookingDto>(false, "Invalid booking data", bookingDto));
            }

            var booking = _mapper.Map<HotelBooking>(bookingDto);
            await _bookingService.AddAsync(booking);
            var createdBookingDto = _mapper.Map<HotelBookingDto>(booking);

            return CreatedAtAction(nameof(GetHotelBooking), new { id = createdBookingDto.Id }, new GeneralResponse<HotelBookingDto>(true, "Booking added successfully", createdBookingDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBooking>>> PutBooking(int id, [FromBody] HotelBookingDto bookingDto)
        {
            if (id != bookingDto.Id)
            {
                return BadRequest(new GeneralResponse<HotelBooking>(false, "Booking ID mismatch", null));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<HotelBookingDto>(false, "Invalid booking data", bookingDto));
            }

            var booking = _mapper.Map<HotelBooking>(bookingDto);
            await _bookingService.UpdateAsync(booking);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBooking>>> DeleteHotelBooking(int id, [FromQuery] string[] includeProperties)
        {
            var existingBooking = await _bookingService.GetAsync(b => b.Id == id, includeProperties);
            if (existingBooking == null)
            {
                return NotFound(new GeneralResponse<HotelBooking>(false, "Booking not found", null));
            }

            await _bookingService.DeleteAsync(id);
            var bookingDTO = _mapper.Map<HotelBookingViewDTO>(existingBooking);
            return Ok(new GeneralResponse<HotelBookingViewDTO>(true, "Booking deleted successfully", bookingDTO));
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] PaymentRequest paymentRequest)
        {
            // Create the booking
            var bookingDto = paymentRequest.Booking;
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<HotelBookingDto>(false, "Invalid booking data", bookingDto));
            }

            var booking = _mapper.Map<HotelBooking>(bookingDto);
            await _bookingService.AddAsync(booking);

            // Process the payment
            var result = await _braintreeService.MakePaymentAsync(paymentRequest.Nonce, booking.TotalPrice);
            if (result.IsSuccess())
            {
                // Update booking with payment info
                //booking.PaymentTransactionId = result.Transaction.Id;
                //booking.PaymentStatus = result.Transaction.Status.ToString();
                booking.Status = BookingStatus.Confirmed;
                await _bookingService.UpdateAsync(booking);

                var createdBookingDto = _mapper.Map<HotelBookingDto>(booking);
                return Ok(new GeneralResponse<HotelBookingDto>(true, "Booking and payment successful", createdBookingDto));
            }
            else
            {
                // Payment failed, return error
                await _bookingService.DeleteAsync(booking.Id); // Optionally, delete the booking if payment fails
                return BadRequest(new { Errors = result.Errors.DeepAll() });
            }
        }

        [HttpGet("client_token")]
        public async Task<IActionResult> GetClientToken()
        {
            var clientToken = await _braintreeService.GetClientTokenAsync();
            return Ok(new { clientToken });
        }
    }
    public class PaymentRequest
    {
        public string Nonce { get; set; }
        public decimal Amount { get; set; }
        public HotelBookingDto Booking { get; set; }
    }
}
