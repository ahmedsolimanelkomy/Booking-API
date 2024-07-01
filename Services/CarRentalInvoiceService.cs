using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class CarRentalInvoiceService : Service<CarRentalInvoice>, ICarRentalInvoiceService
    {
        public CarRentalInvoiceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
