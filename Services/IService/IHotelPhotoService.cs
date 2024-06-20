using Booking_API.Models;

namespace Booking_API.Services.IService
{
    public interface IHotelPhotoService :IService<HotelPhoto>
    {
        Task<string> SavePhoto(IFormFile photo);
        void DeletePhoto(string photoUrl);
        Task<byte[]> GetPhotoFileContent(string photoUrl);
        Task<List<byte[]>> GetAllPhotos();
        Task<IFormFile> ConvertByteArrayToIFormFile(byte[] byteArray, string? fileName = null, string contentType= "image/jpeg");


    }
}
