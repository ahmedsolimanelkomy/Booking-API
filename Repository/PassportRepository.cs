using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class PassportRepository : Repository<Passport>, IPassportRepository
    {
        private readonly BookingContext _dbcontext;
        public PassportRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
