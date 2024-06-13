using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Booking_API.Repository.IRepository;
using Booking_API.Models;
using Booking_API.DTOs;
using AutoMapper;
using Booking_API.Services.IService;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelPhotoController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHotelPhotoService _hotelPhotoService;
        private readonly IMapper _mapper;
        public HotelPhotoController(IWebHostEnvironment hostingEnvironment, IHotelPhotoService hotelPhotoService, IMapper mapper)
        {
            _hostingEnvironment = hostingEnvironment;
          

            _hotelPhotoService = hotelPhotoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] HotelPhotoDTO hotelPhotoDTO, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = Path.Combine("/uploads", fileName);

            if (hotelPhotoDTO != null)
            {
                hotelPhotoDTO.PhotoUrl = imageUrl;
                var hotelPhoto = _mapper.Map<HotelPhoto>(hotelPhotoDTO);
                await _hotelPhotoService.AddAsync(hotelPhoto);
                var createdPhotoDTO = _mapper.Map<HotelPhotoDTO>(hotelPhoto);
                return Ok(new GeneralResponse<HotelPhotoDTO>(true, "Photo uploaded successfully.", createdPhotoDTO));
            }

            return BadRequest("Invalid photo data.");
        }

        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetByHotelId(int hotelId)
        {
            var photos = await _hotelPhotoService.GetPhotosByHotelId(hotelId);
            if (photos == null || !photos.Any())
            {
                return NotFound();
            }

            var photoDTOs = _mapper.Map<IEnumerable<HotelPhotoDTO>>(photos);
            return Ok(new GeneralResponse<IEnumerable<HotelPhotoDTO>>(true, "Photos retrieved successfully.", photoDTOs));
        }

    }
}
