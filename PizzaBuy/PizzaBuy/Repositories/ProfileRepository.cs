using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaBuy.Data;
using PizzaBuy.Models.Domain;

namespace PizzaBuy.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AuthDbContext authDbContext;

        public ProfileRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<Profile> AddAsync(Profile profile)
        {
            await authDbContext.Profiles.AddAsync(profile);
            return profile;
        }

        public Task<Profile?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityUser?> GetAsync(string id)
        {
            return await authDbContext.Users.FindAsync(id.ToString());

        }

        public async Task<Profile?> UpdateAsync(Profile profile)
        {
            // Check if the profile object has a valid ProfileId
            //if (profile.ProfileId == Guid.Empty)
            //{
            //    Console.WriteLine("Invalid ProfileId");
            //    return null;
            //}

            // Retrieve the existing profile by its ProfileId
            var existingProfile = await authDbContext.Profiles.FirstOrDefaultAsync(p => p.ProfileId == profile.UserId);

            if (existingProfile != null)
            {
                // Update the properties of the existing profile
                existingProfile.ProfileName = profile.ProfileName;
                existingProfile.ProfileAge = profile.ProfileAge;

                // Save the changes to the database
                await authDbContext.SaveChangesAsync();

                return existingProfile;
            }

            Console.WriteLine("Profile not found");
            return null;
        }







        public async Task<IdentityUser?> UpdateAsync1(IdentityUser user)
    {
        var existingUser = await authDbContext.Users.FindAsync(user.Id.ToString());

        if (existingUser != null)
        {

            var updatedUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.Email,
                NormalizedEmail = user.Email.Normalize()

            };

            await authDbContext.Users.AddAsync(updatedUser);
            await authDbContext.SaveChangesAsync();
            return updatedUser;


        }
        return null;



    }
    }
}
    