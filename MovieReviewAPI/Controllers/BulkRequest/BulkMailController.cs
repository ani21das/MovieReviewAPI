using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Services.Models;
using User.Services.Services;

namespace MovieReviewAPI.Controllers.BulkMail
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkmailController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public BulkmailController(UserManager<IdentityUser> userManager,
             IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("bulk-email-send-db")]
        public IActionResult SendBulkEmail([FromForm] string subject, [FromForm] string body)
        {
            try
            {
                var userEmails = _userManager.GetUsersInRoleAsync("User").Result.Select(user => user.Email).ToList();

                var commonMessage = new Message(userEmails, subject, body);

                _emailService.SendBulkEmails(new List<Message> { commonMessage });

                return Ok(new { Message = "Bulk emails sent successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Error: {ex.Message}" });
            }
        }
    }
}