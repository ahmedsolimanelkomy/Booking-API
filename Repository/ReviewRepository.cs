using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class ReviewRepository : Repository<HotelReview>, IReviewRepository
    {
        private readonly BookingContext _dbcontext;
        public ReviewRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
