using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelBookingDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using Braintree;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentMethod = Booking_API.Models.PaymentMethod;

namespace Booking_API.Services
{
    public class HotelBookingService : Service<HotelBooking>, IHotelBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly IHotelBookingInvoiceService _hotelBookingInvoiceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HotelBookingService(IUnitOfWork unitOfWork, IMapper mapper, IHotelService hotelService, IRoomService roomService,IHotelBookingInvoiceService hotelBookingInvoiceService, UserManager<ApplicationUser> userManager) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hotelService = hotelService;
            _roomService = roomService;
            _hotelBookingInvoiceService = hotelBookingInvoiceService;
            _userManager = userManager;
        }

        public async Task<IEnumerable<HotelBookingViewDTO>> GetFilteredBookingsAsync(HotelBookingFilterDTO filter)
        {
            var bookings = await _unitOfWork.HotelBookings.GetAllAsync(new[] { "Rooms", "ApplicationUser", "Hotel" });

            var filteredBookings = bookings.Where(booking =>
                (string.IsNullOrEmpty(filter.HotelName) || booking.Hotel.Name.Contains(filter.HotelName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(filter.UserName) || (booking.ApplicationUser?.FirstName + " " + booking.ApplicationUser?.LastName).Contains(filter.UserName, StringComparison.OrdinalIgnoreCase)) &&
                (!filter.CheckIn.HasValue || !filter.CheckOut.HasValue ||
                    (booking.CheckInDate.Date == filter.CheckIn.Value.Date || booking.CheckOutDate.Date == filter.CheckOut.Value.Date)) &&
                (filter.RoomNumber == null || booking.Rooms.Any(room => room.RoomNumber == filter.RoomNumber)) &&
                (filter.Status == null || booking.Status == filter.Status) &&
                (filter.Price == null || booking.TotalPrice == filter.Price)
            ).ToList();

            return _mapper.Map<IEnumerable<HotelBookingViewDTO>>(filteredBookings);
        }

        ////public async Task<GeneralResponse<CreateHotelBookingDTO>> CreateHotelBookingAsync(CreateHotelBookingDTO bookingDto)
        ////{
        ////    var user = await _userManager.FindByIdAsync(bookingDto.UserId.ToString());
        ////    if (user == null)
        ////    {
        ////        return new GeneralResponse<CreateHotelBookingDTO>(false, "User not found", bookingDto);
        ////    }

        ////    var hotel = await _hotelService.GetAsync(h => h.Id == bookingDto.HotelId);
        ////    if (hotel == null)
        ////    {
        ////        return new GeneralResponse<CreateHotelBookingDTO>(false, "Hotel not found", bookingDto);
        ////    }

        ////    var room = await _roomService.GetAsync(r => r.Id == bookingDto.RoomId, new[] { "HotelBookings" });
        ////    if (room == null)
        ////    {
        ////        return new GeneralResponse<CreateHotelBookingDTO>(false, "Room not available", bookingDto);
        ////    }

        ////    // Ensure HotelBookings collection is initialized
        ////    if (room.HotelBookings == null)
        ////    {
        ////        room.HotelBookings = new List<HotelBooking>();
        ////    }

        ////    // Check if room is available for the given date range
        ////    var isRoomAvailable = !room.HotelBookings.Any(booking =>
        ////        booking.Status == BookingStatus.Confirmed &&
        ////        !(booking.CheckOutDate <= bookingDto.CheckInDate || booking.CheckInDate >= bookingDto.CheckOutDate));

        ////    if (!isRoomAvailable)
        ////    {
        ////        return new GeneralResponse<CreateHotelBookingDTO>(false, "Room is not available for the selected dates", bookingDto);
        ////    }

        ////    var booking = _mapper.Map<HotelBooking>(bookingDto);
        ////    room.HotelBookings.Add(booking);
        ////    await _roomService.UpdateAsync(room);

        ////    var createdBookingDto = _mapper.Map<CreateHotelBookingDTO>(booking);

        ////    return new GeneralResponse<CreateHotelBookingDTO>(true, "Booking added successfully", createdBookingDto);
        ////}

        ////public async Task<GeneralResponse<HotelBookingInvoice>> CreateInvoiceAsync(CreateHotelBookingDTO bookingDto, decimal amount, int userId, PaymentMethod paymentMethod, Transaction transaction)
        ////{
        ////    var invoice = new  HotelBookingInvoice
        ////    {
        ////        Number = transaction.PurchaseOrderNumber,
        ////        Date = DateTime.Now,
        ////        Amount = amount,
        ////        PaymentStatus = PaymentStatus.Completed,
        ////        TransactionId = transaction.Id,
        ////        PaymentMethod = paymentMethod,
        ////        UserId = userId,
        ////        HotelBookingId = bookingDto.Id
        ////    };

        ////    await _hotelBookingInvoiceService.AddAsync(invoice);

        ////    return new GeneralResponse<HotelBookingInvoice>(true, "Invoice created successfully", invoice);
        ////}

        public async Task<GeneralResponse<CreateHotelBookingDTO>> CreateHotelBookingAsync(CreateHotelBookingDTO bookingDto)
        {
            var user = await _userManager.FindByIdAsync(bookingDto.UserId.ToString());
            if (user == null)
            {
                return new GeneralResponse<CreateHotelBookingDTO>(false, "User not found", bookingDto);
            }

            var hotel = await _hotelService.GetAsync(h => h.Id == bookingDto.HotelId);
            if (hotel == null)
            {
                return new GeneralResponse<CreateHotelBookingDTO>(false, "Hotel not found", bookingDto);
            }

            var room = await _roomService.GetAsync(r => r.Id == bookingDto.RoomId, new[] { "HotelBookings" });
            if (room == null)
            {
                return new GeneralResponse<CreateHotelBookingDTO>(false, "Room not available", bookingDto);
            }

            // Ensure HotelBookings collection is initialized
            if (room.HotelBookings == null)
            {
                room.HotelBookings = new List<HotelBooking>();
            }

            // Check if room is available for the given date range
            var isRoomAvailable = !room.HotelBookings.Any(booking =>
                booking.Status == BookingStatus.Confirmed &&
                !(booking.CheckOutDate <= bookingDto.CheckInDate || booking.CheckInDate >= bookingDto.CheckOutDate));

            if (!isRoomAvailable)
            {
                return new GeneralResponse<CreateHotelBookingDTO>(false, "Room is not available for the selected dates", bookingDto);
            }

            var booking = _mapper.Map<HotelBooking>(bookingDto);
            room.HotelBookings.Add(booking);

            try
            {
                await _roomService.UpdateAsync(room);
            }
            catch (Exception ex)
            {
                // Handle exception and possibly return an error response
                return new GeneralResponse<CreateHotelBookingDTO>(false, "Failed to create booking", bookingDto);
            }

            var createdBookingDto = _mapper.Map<CreateHotelBookingDTO>(booking);

            return new GeneralResponse<CreateHotelBookingDTO>(true, "Booking added successfully", createdBookingDto);
        }

        public async Task<GeneralResponse<HotelBookingInvoice>> CreateInvoiceAsync(CreateHotelBookingDTO bookingDto, decimal amount, int userId, PaymentMethod paymentMethod)
        {
            var invoice = new HotelBookingInvoice
            {
                Number = GenerateRandomTransactionNumber(),
                Date = DateTime.Now,
                Amount = amount,
                PaymentStatus = PaymentStatus.Completed,
                TransactionId = GenerateRandomTransactionId(),
                PaymentMethod = paymentMethod,
                UserId = userId,
                HotelBookingId = bookingDto.Id
            };

            try
            {
                await _hotelBookingInvoiceService.AddAsync(invoice);
            }
            catch (Exception ex)
            {
                // Handle exception and possibly return an error response
                return new GeneralResponse<HotelBookingInvoice>(false, "Failed to create invoice", null);
            }

            return new GeneralResponse<HotelBookingInvoice>(true, "Invoice created successfully", invoice);
        }

        private int GenerateRandomTransactionNumber()
        {
            Random random = new Random();
            return random.Next(100000, 999999); // Generates a random number between 100000 and 999999
        }

        private string GenerateRandomTransactionId()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] stringChars = new char[10];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars); // Generates a random alphanumeric string of length 10
        }
    }
}
