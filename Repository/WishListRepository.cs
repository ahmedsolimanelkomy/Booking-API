using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class WishListRepository : Repository<HotelWishList>, IWishListRepository
    {
        private readonly BookingContext _dbcontext;
        public WishListRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
