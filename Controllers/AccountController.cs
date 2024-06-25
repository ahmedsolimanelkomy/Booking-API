using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper , IEmailService emailService)
        {
            this.roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpPost("register/user")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<string>(false, "Invalid model data", null));
            }

            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                return Conflict(new GeneralResponse<string>(false, "User with this email already exists", null));
            }

            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                AddRole();
                await _userManager.AddToRoleAsync(user, "user");

                await SendConfirmationEmail(model.Email, user);

                return Ok(new GeneralResponse<string>(true, "User registered successfully", null));
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return BadRequest(new GeneralResponse<string>(false, errors, null));
            }
        }

        bool AddRole()
        {

            if (!roleManager.RoleExistsAsync("USER").Result)
            {
                var UserRole = new ApplicationRole
                {
                    Name = "USER"
                };
                roleManager.CreateAsync(UserRole).Wait();
            }

            if (!roleManager.RoleExistsAsync("ADMIN").Result)
            {
                var AdminRole = new ApplicationRole
                {
                    Name = "ADMIN"
                };
                roleManager.CreateAsync(AdminRole).Wait();
            }
            return true;
        }

        private async Task SendConfirmationEmail(string? email, ApplicationUser? user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"https://BookingBoo.com/confirm-email?UserId={user.Id}&Token={token}";
            await _emailService.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.", true);
        }
         
        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest(new GeneralResponse<string>(false, "UserId or token is null", null));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new GeneralResponse<string>(false, $"User with Id '{userId}' not found", null));
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new GeneralResponse<string>(true, "Email confirmed successfully", null));
            }

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return BadRequest(new GeneralResponse<string>(false, errors, null));
        }


        [HttpPost("register/admin")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> RegisterAdmin([FromBody] UserRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new GeneralResponse<string>(false, "Invalid model data", null));
            }

            var user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "ADMIN");
                return Ok(new GeneralResponse<string>(true, "Admin registered successfully", null));
            }

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return BadRequest(new GeneralResponse<string>(false, errors, null));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid model data", ispass = false });
            }

            var user = await _userManager.FindByNameAsync(loginUser.UserName);
            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, loginUser.Password);
                if (found)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecKey"]));
                    var signingCredentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIss"],
                        audience: _configuration["JWT:ValidAud"],
                        expires: DateTime.Now.AddDays(2),
                        claims: claims,
                        signingCredentials: signingCredentials
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expired = token.ValidTo,
                        ispass = true
                    });
                }
            }

            return Unauthorized(new
            {
                message = "Invalid username or password",
                ispass = false
            });
        }

        [HttpDelete("remove-user/{Email}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> RemoveUser(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return NotFound(new GeneralResponse<string>(false, "User not found", null));
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new GeneralResponse<string>(true, "User removed successfully", null));
            }

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return BadRequest(new GeneralResponse<string>(false, errors, null));
        }

    }
}
