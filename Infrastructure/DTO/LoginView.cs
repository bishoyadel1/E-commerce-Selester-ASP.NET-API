using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class LoginView
    {
        [Required(ErrorMessage = "please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage ="please enter your password")]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
