using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Models
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required, MinLength(8, ErrorMessage = "Please enter atleast 8 characters")]
        public string Password { get; set; } = string.Empty; 
        
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
