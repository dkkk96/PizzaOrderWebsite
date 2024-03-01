using System.ComponentModel.DataAnnotations;

namespace PizzaBuy.Models.Domain
{
    public class ContactUs
    {
       
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
