using Booking_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly string _gmailUsername = "your.gmail@gmail.com"; // Replace with your Gmail address
        private readonly string _gmailPassword = "your_gmail_password"; // Replace with your Gmail password

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ContactMessageDto messageDto)
        {
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_gmailUsername, _gmailPassword);

                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_gmailUsername);
                    mailMessage.To.Add("your.company@gmail.com"); // Replace with your company's email address
                    mailMessage.Subject = messageDto.Subject;
                    mailMessage.Body = $"From: {messageDto.Name} <{messageDto.Email}>\n\n{messageDto.Message}";

                    await client.SendMailAsync(mailMessage);
                }

                return Ok("Message sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send message: {ex.Message}");
            }
        }
    }
}
