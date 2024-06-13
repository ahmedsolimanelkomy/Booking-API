using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using System.Linq.Expressions;

namespace Booking_API.Services
{
    public class HotelService : Service<Hotel>, IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Hotel> AddDTOAsync(HotelDTO hotelDTO)
        {
            
            var hotel = _mapper.Map<Hotel>(hotelDTO);

            
            await _unitOfWork.GetRepository<Hotel>().AddAsync(hotel);
            await _unitOfWork.SaveAsync();

            return hotel;
        }

        public async Task<Hotel> UpdateDTOAsync(HotelDTO hotelDTO)
        {
            var hotel = await _unitOfWork.GetRepository<Hotel>().GetAsync(h=>h.Id==hotelDTO.Id);
            if (hotel == null)
            {
                throw new KeyNotFoundException("Hotel not found");
            }

            _mapper.Map(hotelDTO, hotel);
            await base.UpdateAsync(hotel);
            return hotel;
        }

        
    }
}