using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class PassportService : Service<Passport>, IPassportService
    {
        public PassportService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
