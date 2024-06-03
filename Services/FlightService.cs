using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class FlightService : Service<Flight>, IFlightService
    {
        public FlightService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
