//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using PizzaBuy.Models.Domain;
//using PizzaBuy.Models.ViewModel;
//using PizzaBuy.Repositories; // Add the namespace for your ProfileRepository

//namespace PizzaBuy.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<IdentityUser> userManager;
//        private readonly SignInManager<IdentityUser> signInManager;
//        private readonly IProfileRepository profileRepository;

//        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IProfileRepository profileRepository)
//        {
//            this.userManager = userManager;
//            this.signInManager = signInManager;
//            this.profileRepository = profileRepository;
//        }

//        [HttpGet]
//        public ActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
//        {
//            var identityUser = new IdentityUser
//            {
//                UserName = registerViewModel.Username,
//                Email = registerViewModel.Email
//            };
//            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);
//            if (identityResult.Succeeded)
//            {
//                // Create a Profile for the user
//                var profile = new Profile
//                {
//                    ProfileId = Guid.NewGuid().ToString(),
//                    ProfileName = "DefaultProfileName", // You may set a default name or customize this based on your requirements
//                    ProfileAge = 0, // You may set a default age or customize this based on your requirements
//                    UserId = identityUser.Id
//                };

//                await profileRepository.AddAsync(profile);

//                // Assign User Role if success
//                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
//                if (roleIdentityResult.Succeeded)
//                {
//                    // Show success notification
//                    return RedirectToAction("Register");
//                }
//            }
//            // Show error notification
//            return RedirectToAction("Register");
//        }

//        [HttpGet]
//        public ActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
//        {
//            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

//            if (signInResult.Succeeded)
//            {
//                // Retrieve the ProfileId for the logged-in user
//                var user = await userManager.FindByNameAsync(loginViewModel.Username);
//                var profile = await profileRepository.GetAsync(user.Id.ToString());

//                // Use the profile data as needed
//                // ViewBag.ProfileId = profile?.ProfileId;

//                return RedirectToAction("Index", "Home");
//            }

//            // Show error notification
//            return View();
//        }

//        [HttpGet]
//        public async Task<IActionResult> Logout()
//        {
//            await signInManager.SignOutAsync();
//            return RedirectToAction("Index", "Home");
//        }

//        [HttpGet]
//        public IActionResult AccessDenied()
//        {
//            return View();
//        }
//    }
//}


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaBuy.Models.Domain;
using PizzaBuy.Models.ViewModel;
using PizzaBuy.Repositories;
using System;
using System.Threading.Tasks;

namespace PizzaBuy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IProfileRepository profileRepository;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IProfileRepository profileRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.profileRepository = profileRepository;
        }

        [HttpGet]
        public ActionResult Register() => View();

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Create a Profile for the user
                var profile = new Profile
                {
                    ProfileId = Guid.NewGuid().ToString(),
                    ProfileName = "DefaultProfileName",
                    ProfileAge = 0,
                    UserId = user.Id
                };

                await profileRepository.AddAsync(profile);

                await userManager.AddToRoleAsync(user, "User");

                // Show success notification
                return RedirectToAction("Register");
            }

            // Show error notification
            ModelState.AddModelError(string.Empty, "Registration failed.");
            return View();
        }

        [HttpGet]
        public ActionResult Login() => View();

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var profile = await profileRepository.GetAsync(user.Id);

                    // Use the profile data as needed
                    // ViewBag.ProfileId = profile?.ProfileId;

                    return RedirectToAction("Index", "Home");
                }
            }

            // Show error notification
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied() => View();
    }
}

