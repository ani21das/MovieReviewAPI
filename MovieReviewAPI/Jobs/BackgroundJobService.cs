//using Hangfire;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Linq;
//using User.Services.Models;
//using User.Services.Services;

//namespace MovieReviewAPI.Jobs
//{
//    public class BackgroundJobService
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public BackgroundJobService(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        [AutomaticRetry(Attempts = 3)]
//        public void SendBulkEmailJob(string subject, string body, DateTime sendDateTime)
//        {
//            try
//            {
//                using (var scope = _serviceProvider.CreateScope())
//                {
//                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
//                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

//                    var userEmails = userManager.GetUsersInRoleAsync("User").Result.Select(user => user.Email).ToList();
//                    var commonMessage = new Message(userEmails, subject, body);

//                    emailService.SendBulkEmails(new List<Message> { commonMessage });
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error in SendBulkEmailJob: {ex.Message}");
//                throw;
//            }
//        }

//        public void ScheduleBulkEmail(DateTime sendDateTime, string subject, string body)
//        {
//            // Use Cron format for specifying the schedule
//            var cronExpression = Cron.Daily(sendDateTime.Hour, sendDateTime.Minute);

//            // Generate a unique recurring job ID
//            var recurringJobId = $"SendBulkEmailJob_{Guid.NewGuid()}";

//            // Configure options if needed
//            var recurringJobOptions = new RecurringJobOptions
//            {
//                TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")

//            };

//            // Schedule the job to run at the specified date and time using Cron
//            RecurringJob.AddOrUpdate(recurringJobId, () => SendBulkEmailJob(subject, body, sendDateTime), cronExpression, recurringJobOptions);
//        }
//    }
//}
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using User.Services.Models;
using User.Services.Services;

namespace MovieReviewAPI.Jobs
{
    public class BackgroundJobService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundJobService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ScheduleBulkEmail(DateTime sendDateTime, string subject, string body)
        {
            try
            {
                // Log the scheduled time to check if it matches expectations
                Console.WriteLine($"Scheduling job for {sendDateTime}.");

                // Use Cron format for specifying the schedule
                var cronExpression = $"{sendDateTime.Minute} {sendDateTime.Hour} * * *";

                // Generate a unique recurring job ID
                var recurringJobId = $"SendBulkEmailJob_{Guid.NewGuid()}";

                // Configure options if needed
                var recurringJobOptions = new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata")
                };

                // Schedule the job to run at the specified date and time using Cron
                RecurringJob.AddOrUpdate(recurringJobId, () => SendBulkEmailJob(subject, body), cronExpression, recurringJobOptions);

                Console.WriteLine($"Job scheduled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ScheduleBulkEmail: {ex.Message}");
                throw;
            }
        }

        [AutomaticRetry(Attempts = 3)]
        public void SendBulkEmailJob(string subject, string body)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    var userEmails = userManager.GetUsersInRoleAsync("User").Result.Select(user => user.Email).ToList();
                    var commonMessage = new Message(userEmails, subject, body);

                    emailService.SendBulkEmails(new List<Message> { commonMessage });

                    Console.WriteLine($"Bulk email sent successfully at {DateTime.UtcNow}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendBulkEmailJob: {ex.Message}");
                throw;
            }
        }

    }
}
