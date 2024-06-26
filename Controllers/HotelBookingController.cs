﻿using AutoMapper;
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
        
        [HttpGet("filtered")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelBookingViewDTO>>>> GetFilteredHotelsAsync([FromQuery] HotelBookingFilterDTO filter)
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
            room.HotelBooking = booking;
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

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] PaymentRequest paymentRequest)
        {
            if (paymentRequest == null || paymentRequest.BookingData == null)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invalid data", null));
            }
            // Create the booking
            var bookingDto = paymentRequest?.BookingData;
            
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<CreateHotelBookingDTO>(false, "Invalid booking data", bookingDto));
            }

            // Process the payment
            var result = await _braintreeService.MakePaymentAsync(paymentRequest.Nonce, paymentRequest.Amount);
            if (result.IsSuccess())
            {
                // Update booking with payment info *****************
                bookingDto.Status = BookingStatus.Confirmed;
                await _bookingService.CreateHotelBookingAsync(bookingDto);

                return Ok(new GeneralResponse<CreateHotelBookingDTO>(true, "Booking and payment successful", null));
            }
            else
            {
                // Payment failed, return error
                //await _bookingService.DeleteAsync(booking.Id); // Optionally, delete the booking if payment fails
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
        public CreateHotelBookingDTO BookingData { get; set; }
    }
}
