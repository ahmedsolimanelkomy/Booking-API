using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelPhotosDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelPhotoController : ControllerBase
    {
        private readonly IHotelPhotoService _hotelPhotoService;
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public HotelPhotoController(IHotelPhotoService hotelPhotoService,IHotelService hotelService, IMapper mapper)
        {
            _hotelPhotoService = hotelPhotoService;
            _hotelService = hotelService;
            _mapper = mapper;
        }

        [HttpGet("/api/GetAllHotelsPhotos")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<byte[]>>>> GetAllHotelsPhotos([FromQuery] string[] includeProperties)
        {
            var photos = await _hotelPhotoService.GetAllPhotos();
            
            return Ok(new GeneralResponse<IEnumerable<byte[]>>(true, "All HotelPhotos retrieved successfully", photos));
        }

        [HttpGet("/api/GetHotelPhotos/{hotelId:int}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<GetHotelPhotoDTO>>>> GetHotelPhotos(int hotelId)
        {
            Hotel hotel = await _hotelService.GetAsync(h => h.Id == hotelId);

            if (hotel == null)
            {
                return NotFound(new GeneralResponse<IEnumerable<HotelPhotoDTO>>(false, "Hotel Not Found", null));
            }

            var hotelPhotos = await _hotelPhotoService.GetListAsync(h => h.HotelId == hotelId);
            if (hotelPhotos.Count() == 0 || hotelPhotos == null)
            {
                return Ok(new GeneralResponse<IEnumerable<HotelPhotoDTO>>(true, "Hotel Have No Photos", null));
            }

            var responseDTOs = new List<GetHotelPhotoDTO>();
            foreach (var photo in hotelPhotos)
            {
                var photoContent = await _hotelPhotoService.GetPhotoFileContent(photo.PhotoUrl);
                var photoDTO = _mapper.Map<GetHotelPhotoDTO>(photo);
                photoDTO.Photo = photoContent;
                responseDTOs.Add(photoDTO);
            }

            return Ok(new GeneralResponse<IEnumerable<GetHotelPhotoDTO>>(true, "HotelPhotos retrieved successfully", responseDTOs));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelPhotoDTO>>> GetHotelPhoto(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _hotelPhotoService.GetAsync(p => p.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<HotelPhotoDTO>(false, "HotelPhoto not found", null));
            }
            var responseDTO = _mapper.Map<HotelPhotoDTO>(response);
            return Ok(new GeneralResponse<HotelPhotoDTO>(true, "HotelPhoto retrieved successfully", responseDTO));
        }

        [HttpPost("/api/PostHotelPhoto/{hotelId}")]
        public async Task<ActionResult<GeneralResponse<CreateHotelPhotoDTO>>> PostHotelPhoto(int hotelId, [FromForm] CreateHotelPhotoDTO createHotelPhotoDto)
        {
            Hotel hotel = await _hotelService.GetAsync(h => h.Id == hotelId);
            if (hotel == null)
            {
                return NotFound(new GeneralResponse<CreateHotelPhotoDTO>(false, "Hotel Not Found", null));
            }

            if (createHotelPhotoDto.Photo == null)
            {
                return BadRequest(new GeneralResponse<CreateHotelPhotoDTO>(false, "No Photo Received", null));
            }

            var photoUrl = await _hotelPhotoService.SavePhoto(createHotelPhotoDto.Photo);

            if (string.IsNullOrEmpty(photoUrl))
            {
                return BadRequest(new GeneralResponse<CreateHotelPhotoDTO>(false, "Error Saving Photo", null));
            }

            var hotelPhoto = _mapper.Map<HotelPhoto>(createHotelPhotoDto);
            hotelPhoto.PhotoUrl = photoUrl;
            hotel.Photos.Add(hotelPhoto);
            await _hotelService.UpdateAsync(hotel);

            return Ok(new GeneralResponse<CreateHotelPhotoDTO>(true, "HotelPhoto added successfully", createHotelPhotoDto));
        }

        [HttpPost("/api/PostBulkHotelPhotos/{hotelId}")]
        public async Task<ActionResult<GeneralResponse<BulkHotelPhotoDTO>>> PostBulkHotelPhotos(int hotelId, [FromForm] BulkHotelPhotoDTO bulkHotelPhotoDto)
        {
            Hotel hotel = await _hotelService.GetAsync(h => h.Id == hotelId);
            if (hotel == null)
            {
                return NotFound(new GeneralResponse<BulkHotelPhotoDTO>(false, "Hotel Not Found", null));
            }

            if (bulkHotelPhotoDto.Photos == null || !bulkHotelPhotoDto.Photos.Any())
            {
                return BadRequest(new GeneralResponse<BulkHotelPhotoDTO>(false, "No Photos Received", null));
            }

            var photoUrls = new List<string>();
            foreach (var photo in bulkHotelPhotoDto.Photos)
            {
                var photoUrl = await _hotelPhotoService.SavePhoto(photo);

                if (string.IsNullOrEmpty(photoUrl))
                {
                    return BadRequest(new GeneralResponse<BulkHotelPhotoDTO>(false, "Error Saving One or More Photos", null));
                }

                var hotelPhoto = new HotelPhoto
                {
                    PhotoUrl = photoUrl,
                    Category = bulkHotelPhotoDto.Category
                };

                hotel.Photos.Add(hotelPhoto);
                photoUrls.Add(photoUrl);
            }

            await _hotelService.UpdateAsync(hotel);

            return Ok(new GeneralResponse<BulkHotelPhotoDTO>(true, "Hotel Photos added successfully", bulkHotelPhotoDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<UpdateHotelPhotoDTO>>> PutHotelPhoto(int id, [FromForm] UpdateHotelPhotoDTO updateHotelPhotoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<UpdateHotelPhotoDTO>(false, "Invalid data", null));
            }
            int PhotoId = int.Parse(updateHotelPhotoDTO.Id);

            var existingHotelPhoto = await _hotelPhotoService.GetAsync(h => h.Id == PhotoId);
            if (existingHotelPhoto == null)
            {
                return NotFound(new GeneralResponse<UpdateHotelPhotoDTO>(false, "HotelPhoto not found", null));
            }

            var hotel = await _hotelService.GetAsync(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound(new GeneralResponse<UpdateHotelPhotoDTO>(false, "Hotel not found", null));
            }

            try
            {
                if (updateHotelPhotoDTO.Photo != null)
                {
                    _hotelPhotoService.DeletePhoto(existingHotelPhoto.PhotoUrl);
                    var photoUrl = await _hotelPhotoService.SavePhoto(updateHotelPhotoDTO.Photo);
                    existingHotelPhoto.PhotoUrl = photoUrl;
                }

                existingHotelPhoto.Category = updateHotelPhotoDTO.Category;

                var updatedPhoto = _mapper.Map<HotelPhoto>(existingHotelPhoto);

                var existingPhoto = hotel.Photos.FirstOrDefault(p => p.Id == id);
                if (existingPhoto != null)
                {
                    existingPhoto.PhotoUrl = updatedPhoto.PhotoUrl;
                    existingPhoto.Category = updatedPhoto.Category;
                }
                else
                {
                    hotel.Photos.Add(updatedPhoto);
                }

                await _hotelService.UpdateAsync(hotel);

                //var updatedPhotoDTO = _mapper.Map<UpdateHotelPhotoDTO>(updatedPhoto);
                return Ok(new GeneralResponse<UpdateHotelPhotoDTO>(true, "HotelPhoto updated successfully", updateHotelPhotoDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<UpdateHotelPhotoDTO>(false, "An error occurred while updating the photo", null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<HotelPhotoDTO>>> DeleteHotelPhoto(int id)
        {
            var existingHotelPhoto = await _hotelPhotoService.GetAsync(b => b.Id == id);
            if (existingHotelPhoto == null)
            {
                return NotFound(new GeneralResponse<HotelPhotoDTO>(false, "HotelPhoto not found", null));
            }

            _hotelPhotoService.DeletePhoto(existingHotelPhoto.PhotoUrl);
            await _hotelPhotoService.DeleteAsync(id);
            var responseDTO = _mapper.Map<HotelPhotoDTO>(existingHotelPhoto);
            return Ok(new GeneralResponse<HotelPhotoDTO>(true, "HotelPhoto deleted successfully", responseDTO));
        }


        [HttpGet("category/{category}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<HotelPhoto>>>> GetPhotosByCategory(PhotoCategory category)
        {
            var photos = await _hotelPhotoService.GetListAsync(p => p.Category == category);

            if (photos == null || !photos.Any())
            {
                return NotFound(new GeneralResponse<IEnumerable<HotelPhoto>>(
                    success: false,
                    message: "No photos found for the specified category.",
                    data: null
                ));
            }

            return Ok(new GeneralResponse<IEnumerable<HotelPhoto>>(
                success: true,
                message: "Photos retrieved successfully.",
                data: photos
            ));
        }
    }
}
