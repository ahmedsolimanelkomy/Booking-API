namespace Booking_API.Services.IService
{
    public interface IAccountService
    {
        Task<string> SavePhoto(IFormFile photo);
        void DeletePhoto(string photoUrl);
    }
}
