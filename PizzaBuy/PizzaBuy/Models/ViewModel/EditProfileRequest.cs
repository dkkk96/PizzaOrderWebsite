using Microsoft.AspNetCore.Identity;

namespace PizzaBuy.Models.ViewModel
{
    public class EditProfileRequest
    {
        public Guid ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int ProfileAge { get; set; }
        public string ProfileUserName { get; set; }
        public string ProfileEmail { get; set; }


       
        public IdentityUser User { get; set; }
    }
}
