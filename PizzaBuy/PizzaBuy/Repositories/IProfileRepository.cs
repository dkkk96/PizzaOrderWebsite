using Microsoft.AspNetCore.Identity;
using PizzaBuy.Models.Domain;

namespace PizzaBuy.Repositories
{
    public interface IProfileRepository
    {



        Task<IdentityUser?> GetAsync(string id);

        Task<Profile> AddAsync(Profile profile);
        Task<Profile?> UpdateAsync(Profile profile);
        Task<Profile?> DeleteAsync(Guid id);

        Task<IdentityUser?> UpdateAsync1(IdentityUser user);



    }
}
