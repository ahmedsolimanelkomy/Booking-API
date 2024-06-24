using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class HotelBookingService : IHotelBookingService
    {
        private readonly IHotelBookingRepository _repository;
        private readonly IMapper _mapper;

        public HotelBookingService(IHotelBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
