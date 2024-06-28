using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<HotelBookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(new string[] { "Room", "ApplicationUser", "Hotel" });
            return _mapper.Map<IEnumerable<HotelBookingDTO>>(bookings);
        }

        public async Task<HotelBookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(b => b.Id == id, new string[] { "Room", "ApplicationUser", "Hotel" });
            return _mapper.Map<HotelBookingDTO>(booking);
        }

        public async Task<HotelBookingDTO> CreateBookingAsync(HotelBookingDTO bookingDTO)
        {
            var booking = _mapper.Map<HotelBooking>(bookingDTO);
            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<HotelBookingDTO>(booking);
        }

        public async Task<HotelBookingDTO> UpdateBookingAsync(int id, HotelBookingDTO bookingDTO)
        {
            var booking = _mapper.Map<HotelBooking>(bookingDTO);
            var existingBooking = await _unitOfWork.Bookings.GetAsync(b => b.Id == id);
            if (existingBooking == null)
                return null;

            await _unitOfWork.Bookings.UpdateAsync(booking);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<HotelBookingDTO>(existingBooking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _unitOfWork.Bookings.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<FilteredBookingDTO>> GetFilteredBookingsAsync(HotelBookingFilterDTO filter)
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(new string[] { "Room", "ApplicationUser", "Hotel" });

            var filteredBookings = bookings.Where(booking =>
                (filter.HotelId == null || booking.HotelId == filter.HotelId) &&
                (!filter.CheckIn.HasValue || !filter.CheckOut.HasValue ||
                    booking.CheckOutDate < filter.CheckIn ||
                    booking.CheckInDate > filter.CheckOut) &&
                (filter.RoomNumber == null || booking.Room.RoomNumber == filter.RoomNumber) &&
                (filter.UserId == null || booking.UserId == filter.UserId) &&
                (filter.RoomId == null || booking.RoomId == filter.RoomId) &&
                (filter.Status == null || booking.Status == filter.Status)
            ).ToList();

            return _mapper.Map<IEnumerable<FilteredBookingDTO>>(filteredBookings);
        }
    }
}
