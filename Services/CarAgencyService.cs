using AutoMapper;
using Booking_API.DTOs.CarAgencyDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Services
{
    public class CarAgencyService : Service<CarAgency>, ICarAgencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CarAgencyService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }
        //public async Task<IEnumerable<CarAgencyViewDTO>> GetFilteredCarAgencies(CarAgencyFilterDTO carAgencyFilterDTO)
        //{
        //    IEnumerable<CarAgency> CarAgencies = await _unitOfWork.CarAgencies.GetAllAsync(new[] {"City"} );
        //    var FilteredAgencies = CarAgencies.Where(CarAgency => 
        //    (!carAgencyFilterDTO.CityId.HasValue || CarAgency.CityId == carAgencyFilterDTO.CityId) &&
        //    (string.IsNullOrEmpty(carAgencyFilterDTO.Name) || CarAgency.Name.Contains(carAgencyFilterDTO.Name, StringComparison.OrdinalIgnoreCase)) &&
        //    (string.IsNullOrEmpty(carAgencyFilterDTO.Address) || CarAgency.Address.Contains(carAgencyFilterDTO.Address, StringComparison.OrdinalIgnoreCase))
        //    ).ToList();

        //    return _mapper.Map<IEnumerable<CarAgencyViewDTO>>(FilteredAgencies);
        //}

        public async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Assets/Images/CarAgency");
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

            return $"/Assets/Images/CarAgency/{uniqueFileName}";
        }

        public void DeletePhoto(string photoUrl)
        {
            var fileName = Path.GetFileName(photoUrl);
            var filePath = Path.Combine(_env.WebRootPath, "Assets/Images/CarAgency", fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
