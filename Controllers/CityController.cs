using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.CityDTOS;
using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IService<City> _cityService;
        private readonly ICountryService countryService;
        private readonly IMapper _mapper;

        public CityController(IService<City> cityService,ICountryService countryService, IMapper mapper)
        {
            _cityService = cityService;
            this.countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CityDTO>>>> GetCities([FromQuery] string[] includeProperties = null)
        {
            var cities = await _cityService.GetAllAsync(includeProperties);
            var cityDtos = _mapper.Map<IEnumerable<CityDTO>>(cities);
            return Ok(new GeneralResponse<IEnumerable<CityDTO>>(true, "Cities retrieved successfully", cityDtos));
        }
        [HttpGet("/api/GetCountryCites/{countryId}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CityDTO>>>> GetCounrtyCites(int countryId, [FromQuery] string[] includeProperties)
        {
            Country? country = await countryService.GetAsync(c => c.Id == countryId);

            if (country == null)
            {
                return BadRequest(new GeneralResponse<IEnumerable<CityDTO>>(false, "Counrty Not Exist", null));
            }

            var response = await _cityService.GetListAsync(c => c.CountryId == countryId, includeProperties);

            IList<CityDTO> cities = new List<CityDTO>();

            _mapper.Map(response, cities);

            if (response.Count() == 0)
            {
                return NotFound(new GeneralResponse<IEnumerable<CityDTO>>(false, "Country have no Cities", null));
            }

            return Ok(new GeneralResponse<IEnumerable<CityDTO>>(true, "Cities retrieved successfully", cities));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<CityDTO>>> GetCity(int id, [FromQuery] string[] includeProperties = null)
        {
            var city = await _cityService.GetAsync(c => c.Id == id, includeProperties);
            if (city == null)
            {
                return NotFound(new GeneralResponse<CityDTO>(false, "City not found", null));
            }
            var cityDTO = _mapper.Map<CityDTO>(city);
            return Ok(new GeneralResponse<CityDTO>(true, "City retrieved successfully", cityDTO));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<CityDTO>>> PostCity(CityDTO cityDTO)
        {
            var city = _mapper.Map<City>(cityDTO);
            await _cityService.AddAsync(city);
            var createdCityDTO = _mapper.Map<CityDTO>(city);
            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, new GeneralResponse<CityDTO>(true, "City added successfully", createdCityDTO));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<CityDTO>>> PutCity(int id, CityDTO cityDTO)
        {
            if (id != cityDTO.Id)
            {
                return BadRequest(new GeneralResponse<CityDTO>(false, "City ID mismatch", null));
            }
            var city = _mapper.Map<City>(cityDTO);
            await _cityService.UpdateAsync(city);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<CityDTO>>> DeleteCity(int id)
        {
            var existingCity = await _cityService.GetAsync(c => c.Id == id);
            if (existingCity == null)
            {
                return NotFound(new GeneralResponse<CityDTO>(false, "City not found", null));
            }
            await _cityService.DeleteAsync(id);
            var deletedCityDTO = _mapper.Map<CityDTO>(existingCity);
            return Ok(new GeneralResponse<CityDTO>(true, "City deleted successfully", deletedCityDTO));
        }
    }
}
