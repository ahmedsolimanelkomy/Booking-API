using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class HotelService : Service<Hotel>, IHotelService
    {
        public HotelService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}