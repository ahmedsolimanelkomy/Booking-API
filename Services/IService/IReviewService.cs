using Booking_API.DTOs;
using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IReviewService : IService<HotelReview>
    {
        public Task<GeneralResponse<string>> AddReviewAsync(AddHotelReviewDTO ReviewDto);
        public Task<IEnumerable<DisplayHotelReviewDTO>> GetAllReviewsAsync(string[] includeProperties);

        public Task<IEnumerable<DisplayHotelReviewDTO>> GetAllReviewsByHotelIdAsync(int hotelId, string[] includeProperties);
    }
}
