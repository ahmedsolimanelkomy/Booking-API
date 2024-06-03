using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly BookingContext _dbcontext;
        public CityRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
