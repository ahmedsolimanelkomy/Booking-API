using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class PaymentService : Service<Payment>, IPaymentService
    {
        public PaymentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
