using Booking_API.DTOs;
using Booking_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using OtpNet;
namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SmsService _smsService;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthController(SmsService smsService, EmailService emailService, IConfiguration configuration)
        {
            _smsService = smsService;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost("generate-2fa-code")]
        public async Task<IActionResult> Generate2FACode([FromBody] UserDto userDto)
        {
            var totp = new Totp(Encoding.UTF8.GetBytes(userDto.SecretKey));
            var code = totp.ComputeTotp();

            if (!string.IsNullOrEmpty(userDto.PhoneNumber))
            {
                _smsService.SendSms(userDto.PhoneNumber, $"Your 2FA code is {code}");
            }

            if (!string.IsNullOrEmpty(userDto.Email))
            {
                await _emailService.SendEmailAsync(userDto.Email, "Your 2FA Code", $"Your 2FA code is {code}");
            }

            return Ok(new { Message = "2FA code sent successfully" });
        }

        [HttpPost("verify-2fa-code")]
        public IActionResult Verify2FACode([FromBody] Verify2FADto verifyDto)
        {
            var totp = new Totp(Encoding.UTF8.GetBytes(verifyDto.SecretKey));
            bool isValid = totp.VerifyTotp(verifyDto.Code, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);

            if (isValid)
            {
                var token = GenerateJwtToken(verifyDto.User); // Implement this method to generate a JWT
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Invalid 2FA code");
            }
        }

        private string GenerateJwtToken(string user)
        {
            // Implement JWT token generation here
            return "jwt_token";
        }
    }


}

