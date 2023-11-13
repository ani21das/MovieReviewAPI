using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieReviewAPI.Controllers.User
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("employees")]
        public IActionResult Get()
        {
            // You can return a message or data here
            var message = "This is the list of employees.";
            return Ok(new { Message = message });
        }
    }
}
