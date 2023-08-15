using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class ResetPasswordViewModel
    {
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please rewrite the password")]
        [Compare("Password", ErrorMessage = "The password does not match")]
        public string ComparePassword { get; set; } = string.Empty;

    
    }
}
