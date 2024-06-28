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
            var bookings = await _unitOfWork.HotelBookings.GetAllAsync(["Room", "ApplicationUser", "Hotel"]);

            var filteredBookings = bookings.Where(booking =>
                (filter.HotelId == null || booking.HotelId == filter.HotelId) &&
                (!filter.CheckIn.HasValue || !filter.CheckOut.HasValue ||
                    booking.CheckOutDate < filter.CheckIn ||
                    booking.CheckInDate > filter.CheckOut) &&
                (filter.RoomNumber == null || booking.Room?.RoomNumber == filter.RoomNumber) &&
                (filter.UserId == null || booking.UserId == filter.UserId) &&
                (filter.RoomId == null || booking.RoomId == filter.RoomId) &&
                (filter.Status == null || booking.Status == filter.Status)
            ).ToList();

            return _mapper.Map<IEnumerable<HotelBookingViewDTO>>(filteredBookings);
        }
    }
}
