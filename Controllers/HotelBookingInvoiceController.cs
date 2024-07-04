using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.InvoiceDTOS;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingInvoiceController : ControllerBase
    {
        private readonly IHotelBookingInvoiceService _hotelBookingInvoiceService;
        private readonly IMapper _mapper;

        public HotelBookingInvoiceController(IHotelBookingInvoiceService hotelBookingInvoiceService, IMapper mapper)
        {
            _hotelBookingInvoiceService = hotelBookingInvoiceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ViewInvoiceDTO>>>> GetHotelBookingInvoices([FromQuery] string[] includeProperties)
        {
            try
            {
                var response = await _hotelBookingInvoiceService.GetAllAsync(includeProperties);
                var responseDTOs = _mapper.Map<IEnumerable<ViewInvoiceDTO>>(response);
                return Ok(new GeneralResponse<IEnumerable<ViewInvoiceDTO>>(true, "HotelBookingInvoices retrieved successfully", responseDTOs));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<IEnumerable<ViewInvoiceDTO>>(false, ex.Message, null));
            }
        }

        [HttpGet("GetUserHotelBookingInvoices/{id:int}")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<ViewInvoiceDTO>>>> GetUserInvoices(int id, [FromQuery] string[] includeProperties)
        {
            try
            {
                var response = await _hotelBookingInvoiceService.GetListAsync(i => i.UserId == id, includeProperties);
                var responseDTOs = _mapper.Map<IEnumerable<ViewInvoiceDTO>>(response);
                return Ok(new GeneralResponse<IEnumerable<ViewInvoiceDTO>>(true, "HotelBookingInvoices retrieved successfully", responseDTOs));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<IEnumerable<ViewInvoiceDTO>>(false, ex.Message, null));
            }
        }

        [HttpGet("GetInvoiceByBookingId/{bookingId:int}")]
        public async Task<ActionResult<GeneralResponse<ViewInvoiceDTO>>> GetInvoiceByBookingId(int bookingId, [FromQuery] string[] includeProperties)
        {
            try
            {
                var response = await _hotelBookingInvoiceService.GetAsync(b => b.HotelBookingId == bookingId, includeProperties);
                if (response == null)
                {
                    return NotFound(new GeneralResponse<ViewInvoiceDTO>(false, "Invoice not found", null));
                }

                var responseDTO = _mapper.Map<ViewInvoiceDTO>(response);
                return Ok(new GeneralResponse<ViewInvoiceDTO>(true, "Invoice retrieved successfully", responseDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<ViewInvoiceDTO>(false, ex.Message, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<ViewInvoiceDTO>>> GetHotelBookingInvoice(int id, [FromQuery] string[] includeProperties)
        {
            try
            {
                var response = await _hotelBookingInvoiceService.GetAsync(b => b.Id == id, includeProperties);
                if (response == null)
                {
                    return NotFound(new GeneralResponse<ViewInvoiceDTO>(false, "HotelBookingInvoice not found", null));
                }
                var responseDTO = _mapper.Map<ViewInvoiceDTO>(response);
                return Ok(new GeneralResponse<ViewInvoiceDTO>(true, "HotelBookingInvoice retrieved successfully", responseDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<ViewInvoiceDTO>(false, ex.Message, null));
            }
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<ViewInvoiceDTO>>> PostHotelBookingInvoice([FromBody] ViewInvoiceDTO viewInvoiceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<ViewInvoiceDTO>(false, "Invalid data", null));
            }

            try
            {
                var hotelBookingInvoice = _mapper.Map<HotelBookingInvoice>(viewInvoiceDTO);
                await _hotelBookingInvoiceService.AddAsync(hotelBookingInvoice);
                var responseDTO = _mapper.Map<ViewInvoiceDTO>(hotelBookingInvoice);
                return CreatedAtAction(nameof(GetHotelBookingInvoice), new { id = hotelBookingInvoice.Id }, new GeneralResponse<ViewInvoiceDTO>(true, "HotelBookingInvoice added successfully", responseDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<ViewInvoiceDTO>(false, ex.Message, null));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<ViewInvoiceDTO>>> PutHotelBookingInvoice(int id, [FromBody] ViewInvoiceDTO viewInvoiceDTO)
        {
            if (id != viewInvoiceDTO.Id)
            {
                return BadRequest(new GeneralResponse<ViewInvoiceDTO>(false, "HotelBookingInvoice ID mismatch", null));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<ViewInvoiceDTO>(false, "Invalid data", null));
            }

            try
            {
                var hotelBookingInvoice = _mapper.Map<HotelBookingInvoice>(viewInvoiceDTO);
                await _hotelBookingInvoiceService.UpdateAsync(hotelBookingInvoice);
                return Ok(new GeneralResponse<ViewInvoiceDTO>(true, "HotelBookingInvoice updated successfully", viewInvoiceDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<ViewInvoiceDTO>(false, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<ViewInvoiceDTO>>> DeleteHotelBookingInvoice(int id)
        {
            try
            {
                var existingHotelBookingInvoice = await _hotelBookingInvoiceService.GetAsync(b => b.Id == id);
                if (existingHotelBookingInvoice == null)
                {
                    return NotFound(new GeneralResponse<ViewInvoiceDTO>(false, "HotelBookingInvoice not found", null));
                }

                await _hotelBookingInvoiceService.DeleteAsync(id);
                var responseDTO = _mapper.Map<ViewInvoiceDTO>(existingHotelBookingInvoice);
                return Ok(new GeneralResponse<ViewInvoiceDTO>(true, "HotelBookingInvoice deleted successfully", responseDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GeneralResponse<ViewInvoiceDTO>(false, ex.Message, null));
            }
        }
    }
}
