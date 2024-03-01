using System.ComponentModel.DataAnnotations;

namespace PizzaBuy.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6 ,ErrorMessage ="We need to be safe. Add more character to your password")]
        public string Password { get; set; }
        
    }
}
