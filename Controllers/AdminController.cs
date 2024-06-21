using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.AdminDTOS;
using Booking_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public async Task<ActionResult<GeneralResponse<ICollection<AdminDTO>>>> GetAllAdmins()
        {
            ICollection<AdminDTO> Admins = (await UserManager.GetUsersInRoleAsync("Admin"))
                .Select(user => new AdminDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    Gender = user.Gender
                }).ToList();

            if (Admins.Any())
            {
                return NotFound(new GeneralResponse<ICollection<AdminDTO>>(success: false, message: "Admins are null", data: null));
            }
            return Ok(new GeneralResponse<ICollection<AdminDTO>>(success:true ,message:"Admins fetched succefully and not null",data: Admins));
        }
        [HttpPost("AddAdmin")]
        public async Task<ActionResult<GeneralResponse<string>>> AddAdmin([FromBody] CreateAdminDTO createAdminDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new GeneralResponse<string>(success: false, message: "Invalid model state", data: string.Join("; ", errors)));
            }

            var user = Mapper.Map<ApplicationUser>(createAdminDTO);
            var result = await UserManager.CreateAsync(user, createAdminDTO.Password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                return BadRequest(new GeneralResponse<string>(success: false, message: "Error creating user: " + errorMessage, data: null));
            }

            var roleResult = await UserManager.AddToRoleAsync(user, "ADMIN");
            if (!roleResult.Succeeded)
            {
                var errorMessage = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                return BadRequest(new GeneralResponse<string>(success: false, message: "Error assigning admin role: " + errorMessage, data: null));
            }

            return Ok(new GeneralResponse<string>(success: true, message: "Admin created successfully", data: user.UserName));
        }


        [HttpGet("GetAdminByID/{id}")]
        public async Task<ActionResult<GeneralResponse<AdminDTO>>> GetAdminById(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new GeneralResponse<AdminDTO>(success: false, message: "Admin not found", data: null));
            }

            AdminDTO admin = new AdminDTO();
            Mapper.Map(user, admin);

            return Ok(new GeneralResponse<AdminDTO>(success: true, message: "Admin fetched successfully", data: admin));
        }


        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<ActionResult<GeneralResponse<string>>> DeleteAdmin(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound(new GeneralResponse<string>(success: false, message: "Admin not found", data: null));
            }
            string AdminUserName = user.UserName;

            var isAdmin = await UserManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin)
            {
                return BadRequest(new GeneralResponse<string>(success: false, message: "User is not an admin", data: AdminUserName));
            }

            // Remove the user
            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GeneralResponse<string>(success: true, message: "Admin Deleted", data: AdminUserName));
                // Removed the duplicate return statement
            }
            else
            {
                return BadRequest(new GeneralResponse<string>(success: false, message: "Error deleting user", data: AdminUserName));
            }
        }



        [HttpPatch("UpdateAdmin/{id}")]
        public async Task<ActionResult<GeneralResponse<AdminDTO>>> UpdateAdmin(string Id, AdminDTO adminDTO)
        {
            var user = await UserManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound(new GeneralResponse<AdminDTO>(success: false, message: "Admin not found", data: null));
            }
            Mapper.Map(adminDTO, user);

            // Update user in the database
            var updateResult = await UserManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest(new GeneralResponse<AdminDTO>(success: false, message: "Error updating user", data: null));
            }

            // Update the password if provided
            if (!string.IsNullOrEmpty(adminDTO.NewPassword))
            {
                var passwordChangeResult = await UserManager.ChangePasswordAsync(user, adminDTO.CurrentPassword, adminDTO.NewPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    return BadRequest(new GeneralResponse<AdminDTO>(success: false, message: "Error changing password", data: null));
                }
            }
            return Ok(new GeneralResponse<AdminDTO>(success: true, message: "User updated successfully", data: adminDTO));
        }

    }
}
