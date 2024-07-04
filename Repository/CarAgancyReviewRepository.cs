using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class CarAgancyReviewRepository : Repository<CarAgencyReview>, ICarAgancyReviewRepository
    {
        private BookingContext _bookingContext;
        public CarAgancyReviewRepository(BookingContext context) : base(context)
        {
            _bookingContext = context;
        }
    }
}
