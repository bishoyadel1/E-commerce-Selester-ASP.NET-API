using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public  class RegisterView
    {

        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(50, ErrorMessage = "The name must be less than 50 characters")]
        [MinLength(5, ErrorMessage = "The name must be greater than 5 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "please enter your password")]
        [MaxLength(50, ErrorMessage = "The password must be less than 50 characters")]
        [MinLength(5, ErrorMessage = "The password must be greater than 5 characters")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please rewrite the password")]
        [Compare("Password", ErrorMessage = "The password does not match")]
        public string ComparePassword { get; set; } = string.Empty;

    }

 
}
