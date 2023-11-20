using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieReviewAPI.Models.Authentication;
using MovieReviewAPI.Models.Authentication.Login;
using MovieReviewAPI.Models.Authentication.Signup;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Services.Models;
using User.Services.Services;

namespace MovieReviewAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, IEmailService emailService,
            SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        //Create User or Admin without profile photo
        //[HttpPost]
        //[Route("registration")]
        //public async Task<IActionResult> Register([FromForm] RegisterUser registerUser, string role)
        //{
        //    var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
        //    if (userExist != null)
        //    {
        //        return StatusCode(StatusCodes.Status403Forbidden,
        //            new Response { Status = "Error", Message = "User already exists!" });
        //    }

        //    IdentityUser user = new()
        //    {
        //        Email = registerUser.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = registerUser.Username,
        //        TwoFactorEnabled = false
        //    };

        //    if (await _roleManager.RoleExistsAsync(role))
        //    {
        //        var result = await _userManager.CreateAsync(user, registerUser.Password);
        //        if (!result.Succeeded)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError,
        //                new Response { Status = "Error", Message = "User Failed to Create" });
        //        }

        //        await _userManager.AddToRoleAsync(user, role);

        //        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
        //        var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
        //        _emailService.SendEmail(message);

        //        return StatusCode(StatusCodes.Status200OK,
        //          new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} SuccessFully" });
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                new Response { Status = "Error", Message = "This Role Doesnot Exist." });
        //    }
        //}


        //Crate new user or admin with Profile photo
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register([FromForm] RegisterUser registerUser,
                                                [FromForm] ProfileImageModel profileImage,
                                                string role)
        {
            try
            {
                // Check if the user already exists
                var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
                if (userExist != null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        new Response { Status = "Error", Message = "User already exists!" });
                }

                // Create a new IdentityUser
                var user = new IdentityUser
                {
                    Email = registerUser.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = registerUser.Username,
                    TwoFactorEnabled = false
                };

                // Check if the specified role exists
                if (await _roleManager.RoleExistsAsync(role))
                {
                    // Create the user with the specified password
                    var result = await _userManager.CreateAsync(user, registerUser.Password);
                    if (!result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new Response { Status = "Error", Message = "User Failed to Create" });
                    }

                    // Add the user to the specified role
                    await _userManager.AddToRoleAsync(user, role);

                    // Process the image file
                    if (profileImage != null && profileImage.ImageFile != null)
                    {
                        // Specify the directory path where you want to save the profile images
                        var imageDirectory = Path.Combine("wwwroot", "profile_images");

                        // Check if the directory exists, create it if not
                        if (!Directory.Exists(imageDirectory))
                        {
                            Directory.CreateDirectory(imageDirectory);
                        }

                        // Save the image to the specified directory
                        var imageName = $"{Guid.NewGuid()}_{profileImage.ImageFile.FileName}";
                        var imagePath = Path.Combine(imageDirectory, imageName);

                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await profileImage.ImageFile.CopyToAsync(stream);
                        }

                        // You can associate the image path with the user or perform additional processing
                    }

                    // Send confirmation email
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                    var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
                    _emailService.SendEmail(message);

                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} SuccessFully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                            new Response { Status = "Error", Message = "This Role Does not Exist." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = $"Exception: {ex.Message}" });
            }
        }


        //Enable Two step authentication
        [HttpPost]
        [Route("enable-2FA")]
        public async Task<IActionResult> EnableTwoFactorAuthentication([FromForm] LoginModel model)
        {
         
            if (model == null || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(new { Message = "Invalid request. Please provide valid user ID and password." });
            }
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return BadRequest(new { Message = "User not found." });
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isValidPassword)
            {
                return BadRequest(new { Message = "Invalid password." });
            }


            if (user.TwoFactorEnabled)
            {
                return BadRequest(new { Message = "Two-factor authentication is already enabled for this user." });
            }


            user.TwoFactorEnabled = true;


            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Failed to update user information." });
            }


            return Ok(new
            {
                Message = "Two-factor authentication has been successfully enabled for the user."
            });
        }

        //Confirm mail by clicking on the mail 
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User Doesnot exist!" });
        }


        //Login using credentials
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user != null)
            {
                if (user.TwoFactorEnabled)
                {
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                    var message = new Message(new string[] { user.Email! }, "OTP Confirmation", token);
                    _emailService.SendEmail(message);

                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"We have sent an OTP to your Email {user.Email} and OTP is {token}" });
                }

                if (await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwtToken = GetToken(authClaims);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        expiration = jwtToken.ValidTo
                    });
                }
            }

            return Unauthorized();
        }


        //JWT settings
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
