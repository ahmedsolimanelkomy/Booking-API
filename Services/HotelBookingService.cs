using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelBookingDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Services
{
    public class HotelBookingService : Service<HotelBooking>, IHotelBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelBookingService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelBookingViewDTO>> GetFilteredBookingsAsync(HotelBookingFilterDTO filter)
        {
            var bookings = await _unitOfWork.HotelBookings.GetAllAsync(new[] { "Room", "ApplicationUser", "Hotel" });

            var filteredBookings = bookings.Where(booking =>
                (string.IsNullOrEmpty(filter.HotelName) || booking.Hotel.Name.Contains(filter.HotelName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(filter.UserName) || (booking.ApplicationUser?.FirstName + " " + booking.ApplicationUser?.LastName).Contains(filter.UserName, StringComparison.OrdinalIgnoreCase)) &&
                (!filter.CheckIn.HasValue || !filter.CheckOut.HasValue ||
                    (booking.CheckInDate.Date == filter.CheckIn.Value.Date || booking.CheckOutDate.Date == filter.CheckOut.Value.Date)) &&
                (filter.RoomNumber == null || booking.Room?.RoomNumber == filter.RoomNumber) &&
                (filter.Status == null || booking.Status == filter.Status) &&
                (filter.Price == null || booking.TotalPrice == filter.Price) // Add price filter here
            ).ToList();

            return _mapper.Map<IEnumerable<HotelBookingViewDTO>>(filteredBookings);
        }

    }
}
