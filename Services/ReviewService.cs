using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class ReviewService : Service<HotelReview>, IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
