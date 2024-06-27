using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Repository;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class HotelBookingService : Service<HotelBooking>, IHotelBookingService
    {
        private readonly IHotelBookingRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HotelBookingService(IHotelBookingRepository repository, IMapper mapper,IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        public async Task<IEnumerable<HotelBookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _repository.GetAllBookingsAsync();
            return _mapper.Map<IEnumerable<HotelBookingDTO>>(bookings);
        }

        public async Task<HotelBookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await _repository.GetBookingByIdAsync(id);
            return _mapper.Map<HotelBookingDTO>(booking);
        }

        public async Task<HotelBookingDTO> CreateBookingAsync(HotelBookingDTO bookingDTO)
        {
            var booking = _mapper.Map<HotelBooking>(bookingDTO);
            var createdBooking = await _repository.CreateBookingAsync(booking);
            return _mapper.Map<HotelBookingDTO>(createdBooking);
        }

        public async Task<HotelBookingDTO> UpdateBookingAsync(int id, HotelBookingDTO bookingDTO)
        {
            var booking = _mapper.Map<HotelBooking>(bookingDTO);
            var updatedBooking = await _repository.UpdateBookingAsync(id, booking);
            return _mapper.Map<HotelBookingDTO>(updatedBooking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _repository.DeleteBookingAsync(id);
        }
        public async Task<IEnumerable<FilteredBookingDTO>> GetFilteredBookingsAsync(HotelBookingFilterDTO filter)
        {
            var bookings = await _repository.GetAllBookingsAsync();

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
