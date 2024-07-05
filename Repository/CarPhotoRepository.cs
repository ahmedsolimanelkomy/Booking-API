using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class CarPhotoRepository : Repository<CarPhoto>, ICarPhotoRepository
    {
        private readonly BookingContext _dbcontext;
        public CarPhotoRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
