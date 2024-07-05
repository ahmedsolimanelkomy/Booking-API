using Booking_API.DTOs.TwoFactorAuthDTOS;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Encodings.Web;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoFactorAuthenticationController : ControllerBase
    {
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtHandler _jwtHandler;
        private readonly UrlEncoder _urlEncoder;

        public TwoFactorAuthenticationController(UserManager<ApplicationUser> userManager, IJwtHandler jwtHandler, UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _urlEncoder = urlEncoder;
        }

        [HttpGet("tfa-setup")]
        public async Task<IActionResult> GetTfaSetup(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                return BadRequest("User does not exist");

            var isTfaEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

            var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (authenticatorKey == null)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }
            var formattedKey = GenerateQRCode(email, authenticatorKey);

            return Ok(new TfaSetupDto
            { IsTfaEnabled = isTfaEnabled, AuthenticatorKey = authenticatorKey, FormattedKey = formattedKey });
        }

        [HttpPost("tfa-setup")]
        public async Task<IActionResult> PostTfaSetup([FromBody] TfaSetupDto tfaModel)
        {
            var user = await _userManager.FindByNameAsync(tfaModel.Email);

            var isValidCode = await _userManager
                .VerifyTwoFactorTokenAsync(user,
                  _userManager.Options.Tokens.AuthenticatorTokenProvider,
                  tfaModel.Code);

            if (isValidCode)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                return Ok(new TfaSetupDto { IsTfaEnabled = true });
            }
            else
            {
                return BadRequest("Invalid code");
            }
        }

        [HttpDelete("tfa-setup")]
        public async Task<IActionResult> DeleteTfaSetup(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            else
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                return Ok(new TfaSetupDto { IsTfaEnabled = false });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

            if (user == null
                || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var isTfaEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

            if (!isTfaEnabled)
            {
                var signingCredentials = _jwtHandler.GetSigningCredentials();
                var claims = _jwtHandler.GetClaims(user);
                var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthResponseDto { IsAuthSuccessful = true, IsTfaEnabled = false, Token = token });
            }

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, IsTfaEnabled = true });
        }

        [HttpPost("login-tfa")]
        public async Task<IActionResult> LoginTfa([FromBody] TfaDto tfaDto)
        {
            var user = await _userManager.FindByNameAsync(tfaDto.Email);

            if (user == null)
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

            var validVerification =
              await _userManager.VerifyTwoFactorTokenAsync(
                 user, _userManager.Options.Tokens.AuthenticatorTokenProvider, tfaDto.Code);
            if (!validVerification)
                return BadRequest("Invalid Token Verification");

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthResponseDto { IsAuthSuccessful = true, IsTfaEnabled = true, Token = token });
        }

        private string GenerateQRCode(string email, string unformattedKey)
        {
            return string.Format(
            AuthenticatorUriFormat,
                _urlEncoder.Encode("Code Maze Two-Factor Auth"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }
    }
}
