using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class FeatureService : Service<Feature>, IFeatureService
    {
        public FeatureService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
