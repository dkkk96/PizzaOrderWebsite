using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PizzaBuy.Models.Domain
{
    public class Profile
    {
        [Key]
        public string ProfileId { get; set; }
        public string ProfileName { get; set; }
        public int ProfileAge { get; set; }

        // Use the same type (string) for the foreign key as in IdentityUser
        public string UserId { get; set; }

        // Navigation
        public IdentityUser User { get; set; }
    }
}
