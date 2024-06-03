using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly BookingContext _dbcontext;
        public CountryRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
