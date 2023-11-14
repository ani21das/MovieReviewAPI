﻿using System.ComponentModel.DataAnnotations;

namespace MovieReviewAPI.Models.Authentication.Signup
{
    public class RegisterUser
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}