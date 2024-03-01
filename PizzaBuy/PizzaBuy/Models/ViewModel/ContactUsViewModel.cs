using System.ComponentModel.DataAnnotations;

namespace PizzaBuy.Models.ViewModel
{
    public class ContactUsViewModel
    {
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
