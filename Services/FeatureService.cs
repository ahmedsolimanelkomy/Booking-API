using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Services
{
    public class FeatureService : Service<Feature>, IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FeatureService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Feature?> GetByNameAsync(string name)
        {
            return await _unitOfWork.GetRepository<Feature>().GetAsync(f => f.Name == name);
        }

    }
}
