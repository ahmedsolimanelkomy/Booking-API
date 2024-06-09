using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class FeatureRepository : Repository<Feature>, IFeatureRepositoey
    {
        private readonly BookingContext _dbcontext;
        public FeatureRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
