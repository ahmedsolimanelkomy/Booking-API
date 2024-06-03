using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly BookingContext _dbcontext;
        public ReviewRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
