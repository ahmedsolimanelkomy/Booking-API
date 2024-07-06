using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController : ControllerBase
    {
        private readonly IPassportService _passportService;
        private readonly IMapper _mapper;

        public PassportController(IPassportService passportService, IMapper mapper)
        {
            _passportService = passportService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<PassportDto>>>> GetPassports([FromQuery] string[] includeProperties)
        {
            var response = await _passportService.GetAllAsync(includeProperties);
            var passportDtos = _mapper.Map<IEnumerable<PassportDto>>(response);
            return Ok(new GeneralResponse<IEnumerable<PassportDto>>(true, "Passports retrieved successfully", passportDtos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<PassportDto>>> GetPassport(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _passportService.GetAsync(p => p.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<PassportDto>(false, "Passport not found", null));
            }

            var passportDto = _mapper.Map<PassportDto>(response);
            return Ok(new GeneralResponse<PassportDto>(true, "Passport retrieved successfully", passportDto));
        }
        [HttpGet("ByUserId/{userId}")]
        public async Task<ActionResult<GeneralResponse<PassportDto>>> GetPassportByUserId(int userId)
        {
            var response = await _passportService.GetAsync(p => p.UserId == userId);
            if (response == null)
            {
                return NotFound(new GeneralResponse<PassportDto>(false, "Passport not found", null));
            }

            var passportDto = _mapper.Map<PassportDto>(response);
            return Ok(new GeneralResponse<PassportDto>(true, "Passport retrieved successfully", passportDto));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<string>>> PostPassport(PassportDto passportCreateDto)
        {
            var passport = _mapper.Map<Passport>(passportCreateDto);
            await _passportService.AddAsync(passport);
            return Ok(new GeneralResponse<string>(true, "Passport added successfully", "Passport added successfully"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<PassportDto>>> PutPassport(int id, [FromBody] PassportDto passportUpdateDto)
        {
            var existingPassport = await _passportService.GetAsync(p => p.Id == id);
            if (existingPassport == null)
            {
                return NotFound(new GeneralResponse<PassportDto>(false, "Passport not found", null));
            }

            // Update existingPassport with data from passportUpdateDto
            _mapper.Map(passportUpdateDto, existingPassport); // Assuming AutoMapper configuration is correct

            await _passportService.UpdateAsync(existingPassport);

            // Map back to PassportDto to return updated data
            var updatedPassportDto = _mapper.Map<PassportDto>(existingPassport);

            return Ok(new GeneralResponse<PassportDto>(true, "Passport updated successfully", updatedPassportDto));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<PassportDto>>> DeletePassport(int id)
        {
            var existingPassport = await _passportService.GetAsync(p => p.Id == id);
            if (existingPassport == null)
            {
                return NotFound(new GeneralResponse<PassportDto>(false, "Passport not found", null));
            }

            await _passportService.DeleteAsync(id);
            var passportDto = _mapper.Map<PassportDto>(existingPassport);
            return Ok(new GeneralResponse<PassportDto>(true, "Passport deleted successfully", passportDto));
        }
    }
}
