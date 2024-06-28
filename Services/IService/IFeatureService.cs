using Booking_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Services.IService
{
    public interface IFeatureService : IService<Feature>
    {
        public Task<Feature?> GetByNameAsync(string name);
        

    }
}
