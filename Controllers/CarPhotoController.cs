using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.CarPhotoDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPhotoController : ControllerBase
    {
        private readonly ICarPhotoService _carPhotoService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarPhotoController(ICarPhotoService carPhotoService, ICarService carService, IMapper mapper)
        {
            _carPhotoService = carPhotoService;
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet("/api/GetAllCarsPhotos")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<byte[]>>>> GetAllCarsPhotos([FromQuery] string[] includeProperties)
        {
            var photos = await _carPhotoService.GetAllPhotos();

            return Ok(new GeneralResponse<IEnumerable<byte[]>>(true, "All CarPhotos retrieved successfully", photos));
        }

        [HttpGet("/api/GetCarPhotos/{carId:int}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<GetCarPhotoDTO>>>> GetCarPhotos(int carId)
        {
            Car car = await _carService.GetAsync(h => h.Id == carId);

            if (car == null)
            {
                return NotFound(new GeneralResponse<IEnumerable<CarPhotoDTO>>(false, "Car Not Found", null));
            }

            var carPhotos = await _carPhotoService.GetListAsync(h => h.CarId == carId);
            if (carPhotos.Count() == 0 || carPhotos == null)
            {
                return Ok(new GeneralResponse<IEnumerable<CarPhotoDTO>>(true, "Car Have No Photos", null));
            }

            var responseDTOs = new List<GetCarPhotoDTO>();
            foreach (var photo in carPhotos)
            {
                var photoContent = await _carPhotoService.GetPhotoFileContent(photo.PhotoUrl);
                var photoDTO = _mapper.Map<GetCarPhotoDTO>(photo);
                photoDTO.Photo = photoContent;
                responseDTOs.Add(photoDTO);
            }

            return Ok(new GeneralResponse<IEnumerable<GetCarPhotoDTO>>(true, "CarPhotos retrieved successfully", responseDTOs));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<CarPhotoDTO>>> GetCarPhoto(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _carPhotoService.GetAsync(p => p.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<CarPhotoDTO>(false, "CarPhoto not found", null));
            }
            var responseDTO = _mapper.Map<CarPhotoDTO>(response);
            return Ok(new GeneralResponse<CarPhotoDTO>(true, "CarPhoto retrieved successfully", responseDTO));
        }

        [HttpPost("/api/PostCarPhoto/{carId}")]
        public async Task<ActionResult<GeneralResponse<CreateCarPhotoDTO>>> PostCarPhoto(int carId, [FromForm] CreateCarPhotoDTO createCarPhotoDto)
        {
            Car car = await _carService.GetAsync(h => h.Id == carId);
            if (car == null)
            {
                return NotFound(new GeneralResponse<CreateCarPhotoDTO>(false, "Car Not Found", null));
            }

            if (createCarPhotoDto.Photo == null)
            {
                return BadRequest(new GeneralResponse<CreateCarPhotoDTO>(false, "No Photo Received", null));
            }

            var photoUrl = await _carPhotoService.SavePhoto(createCarPhotoDto.Photo);

            if (string.IsNullOrEmpty(photoUrl))
            {
                return BadRequest(new GeneralResponse<CreateCarPhotoDTO>(false, "Error Saving Photo", null));
            }

            var carPhoto = _mapper.Map<CarPhoto>(createCarPhotoDto);
            carPhoto.PhotoUrl = photoUrl;
            car.CarPhotos.Add(carPhoto);
            await _carService.UpdateAsync(car);

            return Ok(new GeneralResponse<CreateCarPhotoDTO>(true, "CarPhoto added successfully", createCarPhotoDto));
        }

        [HttpPost("/api/PostBulkCarPhotos/{carId}")]
        public async Task<ActionResult<GeneralResponse<BulkCarPhotoDTO>>> PostBulkCarPhotos(int carId, [FromForm] BulkCarPhotoDTO bulkCarPhotoDto)
        {
            Car car = await _carService.GetAsync(h => h.Id == carId);
            if (car == null)
            {
                return NotFound(new GeneralResponse<BulkCarPhotoDTO>(false, "Car Not Found", null));
            }

            if (bulkCarPhotoDto.Photos == null || !bulkCarPhotoDto.Photos.Any())
            {
                return BadRequest(new GeneralResponse<BulkCarPhotoDTO>(false, "No Photos Received", null));
            }

            var photoUrls = new List<string>();
            foreach (var photo in bulkCarPhotoDto.Photos)
            {
                var photoUrl = await _carPhotoService.SavePhoto(photo);

                if (string.IsNullOrEmpty(photoUrl))
                {
                    return BadRequest(new GeneralResponse<BulkCarPhotoDTO>(false, "Error Saving One or More Photos", null));
                }

                var carPhoto = new CarPhoto
                {
                    PhotoUrl = photoUrl,
                    Category = bulkCarPhotoDto.Category
                };

                car.CarPhotos.Add(carPhoto);
                photoUrls.Add(photoUrl);
            }

            await _carService.UpdateAsync(car);

            return Ok(new GeneralResponse<BulkCarPhotoDTO>(true, "Car Photos added successfully", bulkCarPhotoDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<UpdateCarPhotoDTO>>> PutCarPhoto(int id, [FromForm] UpdateCarPhotoDTO updateCarPhotoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<UpdateCarPhotoDTO>(false, "Invalid data", null));
            }
            int PhotoId = int.Parse(updateCarPhotoDTO.Id);

            var existingCarPhoto = await _carPhotoService.GetAsync(h => h.Id == PhotoId);
            if (existingCarPhoto == null)
            {
                return NotFound(new GeneralResponse<UpdateCarPhotoDTO>(false, "CarPhoto not found", null));
            }

            var car = await _carService.GetAsync(h => h.Id == id);
            if (car == null)
            {
                return NotFound(new GeneralResponse<UpdateCarPhotoDTO>(false, "Car not found", null));
            }

            try
            {
                if (updateCarPhotoDTO.Photo != null)
                {
                    _carPhotoService.DeletePhoto(existingCarPhoto.PhotoUrl);
                    var photoUrl = await _carPhotoService.SavePhoto(updateCarPhotoDTO.Photo);
                    existingCarPhoto.PhotoUrl = photoUrl;
                }

                existingCarPhoto.Category = updateCarPhotoDTO.Category;

                var updatedPhoto = _mapper.Map<CarPhoto>(existingCarPhoto);

                var existingPhoto = car.CarPhotos.FirstOrDefault(p => p.Id == id);
                if (existingPhoto != null)
                {
                    existingPhoto.PhotoUrl = updatedPhoto.PhotoUrl;
                    existingPhoto.Category = updatedPhoto.Category;
                }
                else
                {
                    car.CarPhotos.Add(updatedPhoto);
                }

                await _carService.UpdateAsync(car);

                return Ok(new GeneralResponse<UpdateCarPhotoDTO>(true, "CarPhoto updated successfully", updateCarPhotoDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<UpdateCarPhotoDTO>(false, "An error occurred while updating the photo", null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<CarPhotoDTO>>> DeleteCarPhoto(int id)
        {
            var existingCarPhoto = await _carPhotoService.GetAsync(b => b.Id == id);
            if (existingCarPhoto == null)
            {
                return NotFound(new GeneralResponse<CarPhotoDTO>(false, "CarPhoto not found", null));
            }

            _carPhotoService.DeletePhoto(existingCarPhoto.PhotoUrl);
            await _carPhotoService.DeleteAsync(id);
            var responseDTO = _mapper.Map<CarPhotoDTO>(existingCarPhoto);
            return Ok(new GeneralResponse<CarPhotoDTO>(true, "CarPhoto deleted successfully", responseDTO));
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CarPhoto>>>> GetPhotosByCategory(CarPhotoCat category)
        {
            var photos = await _carPhotoService.GetListAsync(p => p.Category == category);

            if (photos == null || !photos.Any())
            {
                return NotFound(new GeneralResponse<IEnumerable<CarPhoto>>(
                    success: false,
                    message: "No photos found for the specified category.",
                    data: null
                ));
            }

            return Ok(new GeneralResponse<IEnumerable<CarPhoto>>(
                success: true,
                message: "Photos retrieved successfully.",
                data: photos
            ));
        }
    }
}
