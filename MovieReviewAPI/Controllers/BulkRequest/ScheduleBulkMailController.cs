//using Hangfire;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using MovieReviewAPI.Jobs;
//using MovieReviewAPI.Models.BulkMail;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using User.Services.Models;
//using User.Services.Services;

//namespace MovieReviewAPI.Controllers.BulkRequest
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ScheduleBulkMailController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly IEmailService _emailService;
//        private readonly BackgroundJobService _backgroundJobService;

//        public ScheduleBulkMailController(UserManager<IdentityUser> userManager, IEmailService emailService, BackgroundJobService backgroundJobService)
//        {
//            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
//            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
//            _backgroundJobService = backgroundJobService ?? throw new ArgumentNullException(nameof(backgroundJobService));
//        }

//        [HttpPost("schedule")]
//        public IActionResult ScheduleBulkEmail([FromForm] BulkEmailRequest request, [FromForm] DateTime sendDateTime)
//        {
//            try
//            {
//                var userEmails = _userManager.GetUsersInRoleAsync("User").Result.Select(user => user.Email).ToList();

//                var commonMessage = new Message(userEmails, request.Subject, request.Body);

//                // Schedule the job to send bulk emails at the specified date and time
//                _backgroundJobService.ScheduleBulkEmail(sendDateTime, request.Subject, request.Body);

//                return Ok(new { Message = "Bulk emails will be sent at the scheduled time." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Error: {ex.Message}" });
//            }
//        }

//        [HttpPost("schedule/2-min")]
//        public IActionResult ScheduleBulkEmail([FromForm] BulkEmailRequest request)
//        {
//            try
//            {
//                var userEmails = _userManager.GetUsersInRoleAsync("User").Result.Select(user => user.Email).ToList();

//                var commonMessage = new Message(userEmails, request.Subject, request.Body);

//                // Use Cron format to specify a schedule to run every 2 minutes
//                var recurringJobId = $"SendBulkEmailJob_{Guid.NewGuid()}";

//                var recurringJobOptions = new RecurringJobOptions
//                {
//                    TimeZone = TimeZoneInfo.Utc // Adjust the timezone as needed
//                };

//                RecurringJob.AddOrUpdate(recurringJobId, () => _backgroundJobService.SendBulkEmailJob(request.Subject, request.Body, DateTime.UtcNow), "*/2 * * * *", recurringJobOptions);

//                return Ok(new { Message = "Bulk emails will be sent every 2 minutes." });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Error: {ex.Message}" });
//            }
//        }


//    }
//}


using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieReviewAPI.Jobs;
using MovieReviewAPI.Models.BulkMail;
using System;
using User.Services.Models;
using User.Services.Services;

namespace MovieReviewAPI.Controllers.BulkRequest
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleBulkMailController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly BackgroundJobService _backgroundJobService;

        public ScheduleBulkMailController(UserManager<IdentityUser> userManager, IEmailService emailService, BackgroundJobService backgroundJobService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _backgroundJobService = backgroundJobService ?? throw new ArgumentNullException(nameof(backgroundJobService));
        }

        [HttpPost("schedule-daily")]
        public IActionResult ScheduleBulkEmailDaily([FromForm] BulkEmailRequest request, [FromForm] int hour, [FromForm] int minute)
        {
            try
            {
                var userEmails = _userManager.GetUsersInRoleAsync("User").Result.Select(user => user.Email).ToList();

                var commonMessage = new Message(userEmails, request.Subject, request.Body);

                // Schedule the job to send bulk emails daily at the specified hour and minute
                _backgroundJobService.ScheduleBulkEmail(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0), request.Subject, request.Body);

                return Ok(new { Message = $"Bulk emails will be scheduled daily at {hour}:{minute}." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Error: {ex.Message}" });
            }
        }
    }
}
