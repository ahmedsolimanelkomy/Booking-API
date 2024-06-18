using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.AdminDTOS;
using Booking_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booking_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IConfiguration Configuration;
        private readonly IMapper Mapper;

        public AdminController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            UserManager = userManager;
            Configuration = configuration;
            Mapper = mapper;
        }

        [HttpGet("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var Admins = (await UserManager.GetUsersInRoleAsync("Admin")).Select(user =>
            new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.UserName,
                user.Gender,
            }).ToList();
            if (Admins.Any())
            {
                return NotFound("Admins are null");
            }
            return Ok(Admins);
        }
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromBody] CreateAdminDTO createAdminDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = Mapper.Map<ApplicationUser>(createAdminDTO);
            var result = await UserManager.CreateAsync(user, createAdminDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Error creating user");
            }

            var roleResult = await UserManager.AddToRoleAsync(user, "ADMIN");
            if (!roleResult.Succeeded)
            {
                return BadRequest("Error assigning admin role");
            }

            return Ok("Admin created successfully");
        }


        [HttpGet("GetAdminBYID/{id}")]
        public async Task<IActionResult> GetAdminById(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound(new { Msg = "Admin not found." });
            }

            var Admin = new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.UserName,
                user.Gender,
            };

            return Ok(Admin);
        }


        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound(new { Msg = "Admin not found." });
            }
            string AdminUserName = user.UserName;

            var isAdmin = await UserManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin)
            {
                return BadRequest($"User {AdminUserName} is not an admin");
            }

            // Remove the user
            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new { Msg = $"Admin {AdminUserName} Deleted" });
            }
            else
            {
                return BadRequest("Error deleting user");
            }
        }


        [HttpPatch("UpdateAdmin/{id}")]
        public async Task<IActionResult> UpdateAdmin(string Id, AdminDTO adminDTO)
        {
            var user = await UserManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound(new { Msg = "Admin not found." });
            }
            Mapper.Map(adminDTO, user);

            // Update user in the database
            var updateResult = await UserManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest("Error updating user");
            }

            // Update the password if provided
            if (!string.IsNullOrEmpty(adminDTO.NewPassword))
            {
                var passwordChangeResult = await UserManager.ChangePasswordAsync(user, adminDTO.CurrentPassword, adminDTO.NewPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    return BadRequest("Error changing password");
                }
            }

            return Ok("User updated successfully");
        }

    }
}
