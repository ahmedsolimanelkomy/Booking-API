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
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IConfiguration Configuration;
        private readonly IMapper Mapper;

        public AdminController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            this.roleManager = roleManager;
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

            if (!Admins.Any())
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
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new GeneralResponse<string>(false, "Invalid model state", string.Join("; ", errors)));
            }

            var existingUser = await UserManager.FindByEmailAsync(createAdminDTO.Email);
            if (existingUser != null)
            {
                return BadRequest(new GeneralResponse<string>(false, "Email address already exists", null));
            }

            var role = await roleManager.RoleExistsAsync("ADMIN");
            if (!role)
            {
                return BadRequest(new GeneralResponse<string>(false, "Role Admin doesn't exist", null));
            }

            var user = Mapper.Map<ApplicationUser>(createAdminDTO);
            var result = await UserManager.CreateAsync(user, createAdminDTO.Password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join("; ", result.Errors.Select(e => e.Description));
                return BadRequest(new GeneralResponse<string>(false, "Error creating user: " + errorMessage, null));
            }

            var roleResult = await UserManager.AddToRoleAsync(user, "ADMIN");
            if (!roleResult.Succeeded)
            {
                var errorMessage = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                return BadRequest(new GeneralResponse<string>(false, "Error assigning admin role: " + errorMessage, null));
            }

            return Ok(new GeneralResponse<string>(true, "Admin created successfully", user.UserName));
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


        [HttpDelete("DeleteAdminByUserName/{userName}")]
        public async Task<ActionResult<GeneralResponse<string>>> DeleteAdminByUserName(string userName)
        {
            var user = await UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                return NotFound(new GeneralResponse<string>(success: false, message: "Admin not found", data: null));
            }

            var isAdmin = await UserManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin)
            {
                return BadRequest(new GeneralResponse<string>(success: false, message: "User is not an admin", data: user.UserName));
            }

            // Remove the user
            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok(new GeneralResponse<string>(success: true, message: "Admin Deleted", data: user.UserName));
            }
            else
            {
                return BadRequest(new GeneralResponse<string>(success: false, message: "Error deleting user", data: user.UserName));
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



        [HttpPatch("UpdateAdminByUserName/{userName}")]
        public async Task<ActionResult<GeneralResponse<AdminDTO>>> UpdateAdminByUserName(string userName, AdminDTO adminDTO)
        {
            var user = await UserManager.FindByNameAsync(userName);

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
