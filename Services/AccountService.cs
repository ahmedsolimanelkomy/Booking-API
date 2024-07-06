using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IWebHostEnvironment _env;

        public AccountService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Assets/Images/UserPhotos");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            return $"/Assets/Images/UserPhotos/{uniqueFileName}";
        }
        public void DeletePhoto(string photoUrl)
        {
            var fileName = Path.GetFileName(photoUrl);
            var filePath = Path.Combine(_env.WebRootPath, "Assets/Images/UserPhotos", fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
