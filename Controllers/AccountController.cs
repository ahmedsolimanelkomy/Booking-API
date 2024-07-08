using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.AccountDTOS;
using Booking_API.Models;
using Booking_API.Services;
using Booking_API.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;

        public AccountController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper, IEmailService emailService, IAccountService accountService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _accountService = accountService;
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new GeneralResponse<string>(false, "User not found", null));
            }
            var userDto = _mapper.Map<UserDTO>(user); // Assuming you have a UserDTO to map the user data
            return Ok(new GeneralResponse<UserDTO>(true, "User retrieved successfully", userDto));
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDTO model)
        {
            // Check if the model is null
            if (model == null)
            {
                return BadRequest(new GeneralResponse<string>(false, "Request body is null", null));
            }

            // Validate model state
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new GeneralResponse<List<string>>(false, "Invalid model data", modelErrors));
            }

            try
            {
                // Check if user already exists
                var userEmailExist = await _userManager.FindByEmailAsync(model.Email);
                var userNameExist = await _userManager.FindByNameAsync(model.UserName);
                if (userEmailExist != null && userNameExist != null)
                {
                    return Conflict(new GeneralResponse<string>(false, "User with this email already exists", null));
                }

                // Map DTO to ApplicationUser
                var user = _mapper.Map<ApplicationUser>(model);

                // Create user with password
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add 'USER' role to the user
                    await AddRoleToUser(user, "USER");

                    // Send confirmation email
                    await SendConfirmationEmail(model.Email, user);

                    return Ok(new GeneralResponse<string>(true, "User registered successfully", null));
                }
                else
                {
                    // Handle errors if user creation failed
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new GeneralResponse<List<string>>(false, "User registration failed", errors));
                }
            }
            catch (Exception ex)
            {
                // Handle internal server error
                return StatusCode(500, new GeneralResponse<string>(false, "An internal server error occurred", ex.Message));
            }
        }

        // Helper method to add role to user
        private async Task AddRoleToUser(ApplicationUser user, string roleName)
        {
            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest(new { message = "Request body is null", ispass = false });
            }

            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                return BadRequest(new { message = "Invalid model data", errors = modelErrors, ispass = false });
            }

            try
            {
           
                var user = await _userManager.FindByNameAsync(loginUser.UserName);

                if (user == null)
                {
                    return Unauthorized(new { message = "User not found", ispass = false });
                }

                if (user.EmailConfirmed)
                {

                    bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginUser.Password);
                    if (!isPasswordCorrect)
                    {
                        return Unauthorized(new { message = "Invalid password", ispass = false });
                    }

                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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
                        roles = roles,
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                    });

                }
                else
                {
                    return Unauthorized(new
                    {
                        ErrorMsg = "Gmail Not Confirmed"
                    });
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal server error occurred", error = ex.Message, ispass = false });
            }
        }

        [HttpDelete("remove-user/{Email}")]
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

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var result = await AddRoleIfNotExists(roleName);
            if (result)
            {
                return Ok(new { Msg = "Role created successfully" });
            }
            return BadRequest(new { Msg = "Role already exists" });
        }
        private async Task<bool> AddRoleIfNotExists(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new ApplicationRole { Name = roleName };
                var result = await _roleManager.CreateAsync(role);
                return result.Succeeded;
            }
            return false;
        }

        [HttpGet("GetRoles")]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
                return Ok(new GeneralResponse<IEnumerable<string>>(true, "Roles retrieved successfully", roles));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<string>(false, $"An error occurred while retrieving roles {ex.Message}", null));
            }
        }

        [HttpGet("GetUserRolesById/{userId}")]
        public async Task<ActionResult> GetUserRolesById(string userId)
        {
            try
            {
                // Find the user by userId
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(new GeneralResponse<string>(false, $"User with Id '{userId}' not found", null));
                }

                // Get roles of the user
                var roles = await _userManager.GetRolesAsync(user);

                return Ok(new GeneralResponse<IEnumerable<string>>(true, "User roles retrieved successfully", roles));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GeneralResponse<string>(false, $"An error occurred while retrieving user roles: {ex.Message}", null));
            }
        }

        [HttpPut("UpdateUser/{userId}")]
        public async Task<ActionResult> UpdateUser(string userId, [FromForm] UserUpdateDTO model)
        {
            if (model == null)
            {
                return BadRequest(new GeneralResponse<string>(false, "Request body is null", null));
            }

            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                return BadRequest(new GeneralResponse<List<string>>(false, "Invalid model data", modelErrors));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new GeneralResponse<string>(false, "User not found", null));
            }

            // Map the updated fields to the user
            user.FirstName = model.FirstName ?? user.FirstName;
            user.LastName = model.LastName ?? user.LastName;
            user.Email = model.Email ?? user.Email;
            user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;
            user.Gender = model.Gender ?? user.Gender;
            user.Address = model.Address ?? user.Address;
            user.BirthDate = model.BirthDate ?? user.BirthDate;
            user.CityId = model.CityId ?? user.CityId;

            // Handle the photo update
            if (model.Photo != null)
            {
                string newPhotoUrl = await _accountService.SavePhoto(model.Photo);

                if (!string.IsNullOrEmpty(user.PhotoUrl))
                {
                    _accountService.DeletePhoto(user.PhotoUrl);
                }
                user.PhotoUrl = newPhotoUrl;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GeneralResponse<string>(true, "User information updated successfully", null));
            }

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return BadRequest(new GeneralResponse<string>(false, errors, null));
        }



        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                return Ok("Password changed successfully");
            }
            else
            {
                return BadRequest(changePasswordResult.Errors);
            }
        }

        #region Mail Confirmation
        private async Task SendConfirmationEmail(string email, ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var encodedToken = Uri.EscapeDataString(token); // Encode the token
            var confirmationLink = $"http://localhost:4200/confirm-email?UserId={user.Id}&Token={token}";
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
            token = token.Replace(" ", "+");
            token = Uri.UnescapeDataString(token); // Decode the token
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new GeneralResponse<string>(true, "Email confirmed successfully", null));
            }

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return BadRequest(new GeneralResponse<string>(false, errors, null));
        }
        #endregion
    }
}
