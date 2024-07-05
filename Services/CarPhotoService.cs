using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class CarPhotoService : Service<CarPhoto>, ICarPhotoService
    {
        private readonly IWebHostEnvironment _env;

        public CarPhotoService(IUnitOfWork unitOfWork, IWebHostEnvironment env) : base(unitOfWork)
        {
            _env = env;
        }

        public async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Assets/Images/HotelsPhotos");
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

            return $"/Assets/Images/HotelsPhotos/{uniqueFileName}";
        }

        public void DeletePhoto(string photoUrl)
        {
            var fileName = Path.GetFileName(photoUrl);
            var filePath = Path.Combine(_env.WebRootPath, "Assets/Images/HotelsPhotos", fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<byte[]> GetPhotoFileContent(string photoUrl)
        {
            var fileName = Path.GetFileName(photoUrl);
            var filePath = Path.Combine(_env.WebRootPath, "Assets/Images/HotelsPhotos", fileName);

            if (File.Exists(filePath))
            {
                return await File.ReadAllBytesAsync(filePath);
            }

            return null;
        }

        public async Task<List<byte[]>> GetAllPhotos()
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Assets/Images/HotelsPhotos");
            List<byte[]> photos = new List<byte[]>();

            if (Directory.Exists(uploadsFolder))
            {
                var files = Directory.GetFiles(uploadsFolder);
                foreach (var file in files)
                {
                    photos.Add(await File.ReadAllBytesAsync(file));
                }
            }
            return photos;
        }

        public async Task<IFormFile> ConvertByteArrayToIFormFile(byte[] byteArray, string? fileName = null, string contentType = "image/jpeg")
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                IFormFile formFile = new FormFile(memoryStream, 0, byteArray.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };

                return await Task.FromResult(formFile);
            }
        }
    }
}
