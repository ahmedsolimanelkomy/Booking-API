using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelBookingDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Identity;
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
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BraintreeService _braintreeService;
        private readonly IMapper _mapper;

        public HotelBookingController(IHotelBookingService bookingService,IHotelService hotelService, IRoomService roomService,UserManager<ApplicationUser> userManager, BraintreeService braintreeService, IMapper mapper)
        {
            _bookingService = bookingService;
            _hotelService = hotelService;
            _roomService = roomService;
            _userManager = userManager;
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

        [HttpPost("GetFilteredUserBookings")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelBookingViewDTO>>>> GetFilteredUserBookings([FromQuery] UserBookingFilterDTO filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<IEnumerable<HotelBookingViewDTO>>(false, "Invalid data", null));
            }

            try
            {
                var bookingDTOs = await _bookingService.GetFilteredUserBookingsAsync(filter);
                return Ok(new GeneralResponse<IEnumerable<HotelBookingViewDTO>>(true, "Filtered bookings retrieved successfully", bookingDTOs));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<IEnumerable<HotelBookingViewDTO>>(false, ex.Message, null));
            }
        }

        [HttpGet("filtered")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelBookingViewDTO>>>> GetFilteredBookingsAsync([FromQuery] HotelBookingFilterDTO filter)
        {
            var filteredBookings = await _bookingService.GetFilteredBookingsAsync(filter);

            if (filteredBookings == null || !filteredBookings.Any())
            {
                return Ok(new GeneralResponse<IEnumerable<HotelBookingViewDTO>>(true, "No bookings found matching the filter criteria.", filteredBookings));
            }

            return Ok(new GeneralResponse<IEnumerable<HotelBookingViewDTO>>(true, "Bookings retrieved successfully.", filteredBookings));
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
        public async Task<ActionResult<GeneralResponse<CreateHotelBookingDTO>>> PostHotelBooking([FromBody] CreateHotelBookingDTO bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invalid booking data", bookingDto));
            }

            var user = await _userManager.FindByIdAsync(bookingDto.UserId.ToString());
            if (user == null)
            {
                return NotFound(new GeneralResponse<CreateHotelBookingDTO>(false, "User not found", bookingDto));
            }

            var hotel = await _hotelService.GetAsync(h => h.Id == bookingDto.HotelId);
            if (hotel == null)
            {
                return NotFound(new GeneralResponse<CreateHotelBookingDTO>(false, "Hotel not found", bookingDto));
            }

            var room = await _roomService.GetAsync(r => r.Id == bookingDto.RoomId);
            if (room == null)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Room not available", bookingDto));
            }

            var booking = _mapper.Map<HotelBooking>(bookingDto);
            room?.HotelBookings?.Add(booking);
            await _roomService.UpdateAsync(room);

            var createdBookingDto = _mapper.Map<CreateHotelBookingDTO>(booking);

            return CreatedAtAction(nameof(GetHotelBookings), new GeneralResponse<CreateHotelBookingDTO>(true, "Booking added successfully", createdBookingDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelBooking>>> PutBooking(int id, [FromBody] CreateHotelBookingDTO bookingDto)
        {
            //if (id != bookingDto.Id)
            //{
            //    return BadRequest(new GeneralResponse<HotelBooking>(false, "Booking ID mismatch", null));
            //}

            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invalid booking data", bookingDto));
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

        #region Payment
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] PaymentRequest paymentRequest)
        {
            if (paymentRequest == null || paymentRequest.BookingData == null)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invalid data", null));
            }

            var bookingDto = paymentRequest.BookingData;

            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invalid booking data", bookingDto));
            }

            // Process the payment
            var paymentResult = await _braintreeService.MakePaymentAsync(paymentRequest.Nonce, paymentRequest.Amount);

            if (!paymentResult.IsSuccess())
            {
                // Payment failed, return error
                return BadRequest(new { Errors = paymentResult.Errors.DeepAll() });
            }

            // Payment succeeded, proceed with booking
            bookingDto.Status = BookingStatus.Confirmed;
            var bookingResponse = await _bookingService.CreateHotelBookingAsync(bookingDto);

            if (!bookingResponse.Success)
            {
                // Optionally, handle failure to create booking (rollback payment if necessary)
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Booking creation failed", bookingDto));
            }

            // Create the invoice
            var invoiceResponse = await _bookingService.CreateInvoiceAsync(
                bookingResponse.Data,
                paymentRequest.Amount,
                bookingDto.UserId,
                PaymentMethod.PayPal
                );

            if (!invoiceResponse.Success)
            {
                // Handle failure to create invoice by deleting the booking
                var booking = await _bookingService.GetAsync(b => b.Id == bookingResponse.Data.Id);
                await _bookingService.DeleteAsync(booking.Id);

                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invoice creation failed, booking deleted", bookingDto));
            }

            return Ok(new
            {
                Message = "Booking and payment successful"
            });
        }

        [HttpPost("checkout/cash")]
        public async Task<IActionResult> CheckoutCash([FromBody] PaymentRequest paymentRequest)
        {
            if (paymentRequest == null || paymentRequest.BookingData == null)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invalid data", null));
            }

            var bookingDto = paymentRequest.BookingData;

            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invalid booking data", bookingDto));
            }

            // Proceed with booking
            bookingDto.Status = BookingStatus.Confirmed;
            var bookingResponse = await _bookingService.CreateHotelBookingAsync(bookingDto);

            if (!bookingResponse.Success)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Booking creation failed", bookingDto));
            }

            // Create the invoice
            var invoiceResponse = await _bookingService.CreateInvoiceAsync(
                bookingResponse.Data,
                paymentRequest.Amount,
                bookingDto.UserId,
                PaymentMethod.Cash
            );

            if (!invoiceResponse.Success)
            {
                var booking = await _bookingService.GetAsync(b => b.Id == bookingResponse.Data.Id);
                await _bookingService.DeleteAsync(booking.Id);

                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invoice creation failed, booking deleted", bookingDto));
            }

            return Ok(new
            {
                Message = "Booking successful with cash payment"
            });
        }


        [HttpGet("client_token")]
        public async Task<IActionResult> GetClientToken()
        {
            var clientToken = await _braintreeService.GetClientTokenAsync();
            return Ok(new { clientToken });
        } 
        #endregion
    }
    public class PaymentRequest
    {
        public string? Nonce { get; set; }
        public decimal Amount { get; set; }
        public CreateHotelBookingDTO BookingData { get; set; }
    }
}
