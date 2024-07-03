using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Repository;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Services
{
    public class ReviewService : Service<HotelReview>, IReviewService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<GeneralResponse<string>> AddReviewAsync(AddHotelReviewDTO ReviewDto)
        {
            if (ReviewDto == null)
            {
                return new GeneralResponse<string>(false, "Invalid review data", null);
            }

            // Check if the username exists
            var user = await unitOfWork.GetRepository<ApplicationUser>()
                .GetAsync(u => u.Id == ReviewDto.UserId);

            if (user == null)
            {
                return new GeneralResponse<string>(false, "User Id does not exist", null);
            }

            // Check if the hotel ID exists
            var hotel = await unitOfWork.GetRepository<Hotel>()
                .GetAsync(h => h.Id == ReviewDto.HotelId);

            if (hotel == null)
            {
                return new GeneralResponse<string>(false, "Hotel ID does not exist", null);
            }

            ////////
            var reservations = await unitOfWork.GetRepository<HotelBooking>()
                .GetAsync(r => r.UserId == ReviewDto.UserId && r.HotelId == ReviewDto.HotelId);

            if (reservations == null)
            {
                return new GeneralResponse<string>(false, "User hasn't reserved in this hotel so he can't give a review", null);
            }
            ///////////

            HotelReview review = mapper.Map<HotelReview>(ReviewDto);
            await unitOfWork.GetRepository<HotelReview>().AddAsync(review);

            try
            {
                await unitOfWork.SaveAsync();
                return new GeneralResponse<string>(true, "Review added successfully", null);
            }
            catch (Exception ex)
            {
                return new GeneralResponse<string>(false, "Internal server error", ex.Message);
            }
        }

        public async Task<IEnumerable<DisplayHotelReviewDTO>> GetAllReviewsAsync([FromQuery] string[] includeProperties)
        {
            var reviews = await unitOfWork.GetRepository<HotelReview>().GetAllAsync(includeProperties: includeProperties.Concat(new[] { "ApplicationUser" }).ToArray());

            return mapper.Map<IEnumerable<DisplayHotelReviewDTO>>(reviews);
        }


        public async Task<IEnumerable<DisplayHotelReviewDTO>> GetAllReviewsByHotelIdAsync(int hotelId, string[] includeProperties)
        {
            includeProperties = includeProperties.Concat(new[] { "ApplicationUser" }).ToArray();
            var reviews = await unitOfWork.GetRepository<HotelReview>().GetListAsync(
                filter: r => r.HotelId == hotelId,
                includeProperties: includeProperties
            );

            return mapper.Map<IEnumerable<DisplayHotelReviewDTO>>(reviews);
        }

    }
}
