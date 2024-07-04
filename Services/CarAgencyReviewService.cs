using AutoMapper;
using Booking_API.DTOs.CarReviewDTO;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using System.Linq.Expressions;

namespace Booking_API.Services
{
    public class CarAgencyReviewService : Service<CarAgencyReview>, ICarAgencyReviewService
    {
        private  IUnitOfWork _unitOfWork;
        private  IMapper _mapper;
        public CarAgencyReviewService(IUnitOfWork unitOfWork,IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarAgancyReviewDTO>> GetReviewsByCarAgencyIdAsync(int carAgencyId, string[]? includeProperties = null)
        {
            Expression<Func<CarAgencyReview, bool>> filter = review => review.CarAgencyId == carAgencyId;
            var reviews = await GetListAsync(filter, includeProperties);
            return _mapper.Map<IEnumerable<CarAgancyReviewDTO>>(reviews);
        }

        public async Task<CarAgancyReviewDTO> AddReviewAsync(CarAgancyReviewDTO reviewDto)
        {
            var review = _mapper.Map<CarAgencyReview>(reviewDto);
            await AddAsync(review);
            return _mapper.Map<CarAgancyReviewDTO>(review);
        }

        public async Task<CarAgancyReviewDTO> UpdateReviewAsync(CarAgancyReviewDTO reviewDto)
        {
            var review = _mapper.Map<CarAgencyReview>(reviewDto);
            await UpdateAsync(review);
            return _mapper.Map<CarAgancyReviewDTO>(review);
        }
    }

}

