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

        //public async Task<IEnumerable<HotelBookingDTO>> GetAllBookingsAsync(string[] includeProperties = null)
        //{
        //    var bookings = await _unitOfWork.HotelBookings.GetAllAsync(includeProperties);
        //    return _mapper.Map<IEnumerable<HotelBookingDTO>>(bookings);
        //}

        //public async Task<HotelBookingDTO> GetBookingByIdAsync(int id, string[] includeProperties = null)
        //{
        //    var booking = await _unitOfWork.HotelBookings.GetAsync(b => b.Id == id, includeProperties);
        //    return _mapper.Map<HotelBookingDTO>(booking);
        //}

        //public async Task<HotelBookingDto> CreateBookingAsync(HotelBookingDto bookingDTO)
        //{
        //    var booking = _mapper.Map<HotelBooking>(bookingDTO);
        //    await _unitOfWork.HotelBookings.AddAsync(booking);
        //    await _unitOfWork.SaveAsync();
        //    return bookingDTO;
        //}

        //public async Task<HotelBookingDTO> UpdateBookingAsync(int id, HotelBookingDTO bookingDTO)
        //{
        //    var booking = _mapper.Map<HotelBooking>(bookingDTO);
        //    await _unitOfWork.HotelBookings.UpdateAsync(booking);
        //    await _unitOfWork.SaveAsync();
        //    return bookingDTO;
        //}

        //public async Task DeleteBookingAsync(int id)
        //{
        //    await _unitOfWork.HotelBookings.DeleteAsync(id);
        //    await _unitOfWork.SaveAsync();
        //}
    }
}
