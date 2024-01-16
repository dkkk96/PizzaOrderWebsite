using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaBuy.Models.Domain;
using PizzaBuy.Models.ViewModel;
using PizzaBuy.Repositories;

namespace PizzaBuy.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository profileRepository;

        public ProfileController(IProfileRepository profileRepository) 
        {
            this.profileRepository = profileRepository;
        }




        //public IActionResult Index()
        //{
        //    return View();
        //}


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var profile = await profileRepository.GetAsync(id.ToString());

            if (profile != null)
            {
                var model = new EditProfileRequest
                {
                    ProfileName = "",
                    ProfileAge = 0,
                    ProfileEmail = profile.Email,
                    ProfileUserName = profile.UserName,
                    ProfileId = id
                    
                };

                return View(model);




            }
            else
            {
                return View("Edit");
                // If the profile is not found, you might want to redirect or show an error view
                // Example: return NotFound("Profile not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileRequest editProfileRequest)
        {
            var updatedIdentityUser = new IdentityUser
            {
                UserName = editProfileRequest.ProfileUserName, 
                Email = editProfileRequest.ProfileEmail,

            };

            //code to persist the IdentityUser
            await profileRepository.UpdateAsync1(updatedIdentityUser);

            var updatedProfile = new Profile
            {
                ProfileName = editProfileRequest.ProfileName,
                ProfileAge = editProfileRequest.ProfileAge,
                //ProfileId = editProfileRequest.ProfileId.ToString(),

            };

           // await profileRepository.UpdateAsync(updatedProfile);
            
            


            // code to persist the profile

          //  updatedIdentityUser = await profileRepository.UpdateAsync(updatedProfile);
            //updatedProfile = await profileRepository.UpdateAsync(updatedProfile);

            return RedirectToAction("Index","Home");
        }
    }
}
