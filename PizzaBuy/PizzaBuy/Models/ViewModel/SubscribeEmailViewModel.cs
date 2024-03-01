using System.ComponentModel.DataAnnotations;

namespace PizzaBuy.Models.ViewModel
{
    public class SubscribeEmailViewModel
    {
        [Required]
        public string Email1 { get; set; }



        //for contact us
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
