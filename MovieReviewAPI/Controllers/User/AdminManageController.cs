using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminManageController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminManageController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            // Retrieve only users in the "Admin" role
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(string id)
        {
            // Ensure that the user is in the "Admin" role
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(id), "Admin");
            if (!isAdmin)
            {
                return NotFound();
            }

            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(string id, [FromBody] IdentityUser updatedAdmin)
        {
            // Ensure that the user is in the "Admin" role
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(id), "Admin");
            if (!isAdmin)
            {
                return NotFound();
            }

            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            // Update admin properties based on the updatedAdmin object
            admin.Email = updatedAdmin.Email;
            admin.UserName = updatedAdmin.UserName;

            var result = await _userManager.UpdateAsync(admin);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Admin updated successfully." });
            }
            else
            {
                return BadRequest(new { Message = "Failed to update admin.", Errors = result.Errors });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            // Ensure that the user is in the "Admin" role
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(id), "Admin");
            if (!isAdmin)
            {
                return NotFound();
            }

            var admin = await _userManager.FindByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(admin);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Admin deleted successfully." });
            }
            else
            {
                return BadRequest(new { Message = "Failed to delete admin.", Errors = result.Errors });
            }
        }
    }
}
