using Microsoft.AspNetCore.Mvc;
using PizzaBuy.Models;
using PizzaBuy.Models.ViewModel;
using System.Diagnostics;

namespace PizzaBuy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SubscribeEmailViewModel());
        }

        [HttpPost]
        public IActionResult Index(SubscribeEmailViewModel subscribeEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                // Process the subscription or store the email as needed
                TempData["SubscribedEmail"] = subscribeEmailViewModel.Email1;
                TempData["notifyMessage"] = "Subscribed";
                // Redirect to the SubscribeEmail action in the Customer controller
                return RedirectToAction("SubscribeEmail", "Customer");
            }

            // If the model state is not valid, return to the same view with validation errors
            return View(subscribeEmailViewModel);
        }
    

    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
