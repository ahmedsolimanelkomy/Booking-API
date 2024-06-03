using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class AirlineService : Service<Airline>, IAirlineService
    {
        public AirlineService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
