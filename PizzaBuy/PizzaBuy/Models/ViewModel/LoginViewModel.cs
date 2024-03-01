using System.ComponentModel.DataAnnotations;

namespace PizzaBuy.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage ="Your Password has at least 6 characters...")]
        public string Password { get; set; }
    }
}
