using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IService<Country> _countryService;
        private readonly IMapper _mapper;

        public CountryController(IService<Country> countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<CountryDTO>>>> GetCountries([FromQuery] string[] includeProperties = null)
        {
            var countries = await _countryService.GetAllAsync(includeProperties);
            var countriesDTO = _mapper.Map<IEnumerable<CountryDTO>>(countries);
            return Ok(new GeneralResponse<IEnumerable<CountryDTO>>(true, "Countries retrieved successfully", countriesDTO));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<CountryDTO>>> GetCountry(int id, [FromQuery] string[] includeProperties = null)
        {
            var country = await _countryService.GetAsync(c => c.Id == id, includeProperties);
            if (country == null)
            {
                return NotFound(new GeneralResponse<CountryDTO>(false, "Country not found", null));
            }
            var countryDTO = _mapper.Map<CountryDTO>(country);
            return Ok(new GeneralResponse<CountryDTO>(true, "Country retrieved successfully", countryDTO));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<CountryDTO>>> PostCountry(CountryDTO countryDTO)
        {
            var country = _mapper.Map<Country>(countryDTO);
            await _countryService.AddAsync(country);
            var createdCountryDTO = _mapper.Map<CountryDTO>(country);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, new GeneralResponse<CountryDTO>(true, "Country added successfully", createdCountryDTO));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<CountryDTO>>> PutCountry(int id, CountryDTO countryDTO)
        {
            if (id != countryDTO.Id)
            {
                return BadRequest(new GeneralResponse<CountryDTO>(false, "Country ID mismatch", null));
            }
            var country = _mapper.Map<Country>(countryDTO);
            await _countryService.UpdateAsync(country);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<CountryDTO>>> DeleteCountry(int id)
        {
            var existingCountry = await _countryService.GetAsync(c => c.Id == id);
            if (existingCountry == null)
            {
                return NotFound(new GeneralResponse<CountryDTO>(false, "Country not found", null));
            }
            await _countryService.DeleteAsync(id);
            var deletedCountryDTO = _mapper.Map<CountryDTO>(existingCountry);
            return Ok(new GeneralResponse<CountryDTO>(true, "Country deleted successfully", deletedCountryDTO));
        }
    }
}
