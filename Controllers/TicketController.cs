using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _TicketService;

        public TicketController(ITicketService TicketService)
        {
            _TicketService = TicketService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<Ticket>>>> GetTickets([FromQuery] string[] includeProperties)
        {
            var response = await _TicketService.GetAllAsync(includeProperties);
            return Ok(new GeneralResponse<IEnumerable<Ticket>>(true, "Tickets retrieved successfully", response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<Ticket>>> GetTicket(int id, [FromQuery] string[] includeProperties)
        {
            var response = await _TicketService.GetAsync(b => b.Id == id, includeProperties);
            if (response == null)
            {
                return NotFound(new GeneralResponse<Ticket>(false, "Ticket not found", null));
            }
            return Ok(new GeneralResponse<Ticket>(true, "Ticket retrieved successfully", response));
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Ticket>>> PostTicket(Ticket Ticket)
        {
            await _TicketService.AddAsync(Ticket);
            return CreatedAtAction(nameof(GetTicket), new { id = Ticket.Id }, new GeneralResponse<Ticket>(true, "Ticket added successfully", Ticket));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GeneralResponse<Ticket>>> PutTicket(int id, Ticket Ticket)
        {
            if (id != Ticket.Id)
            {
                return BadRequest(new GeneralResponse<Ticket>(false, "Ticket ID mismatch", null));
            }

            await _TicketService.UpdateAsync(Ticket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<Ticket>>> DeleteTicket(int id)
        {
            var existingTicket = await _TicketService.GetAsync(b => b.Id == id);
            if (existingTicket == null)
            {
                return NotFound(new GeneralResponse<Ticket>(false, "Ticket not found", null));
            }

            await _TicketService.DeleteAsync(id);
            return Ok(new GeneralResponse<Ticket>(true, "Ticket deleted successfully", existingTicket));
        }
    }
}
