using Microsoft.AspNetCore.Mvc;
using PizzaBuy.Data;
using PizzaBuy.Models.Domain;
using PizzaBuy.Models.ViewModel;

namespace PizzaBuy.Controllers
{
    public class CustomerController : Controller
    {
        private readonly PizzaBuyDbContext pizzaBuyDbContext;

        public CustomerController(PizzaBuyDbContext pizzaBuyDbContext)
        {
            this.pizzaBuyDbContext = pizzaBuyDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> SubscribeEmail(SubscribeEmailViewModel subscribeEmailViewModel)
        {
            string email = TempData["SubscribedEmail"] as string;
            

            if (email != null)
            {
                var result = new SubscribeEmail
                {
                    Email = email
                };
                await pizzaBuyDbContext.AddAsync(result);
                await pizzaBuyDbContext.SaveChangesAsync(); // Assuming you want to save changes to the database
                TempData["notifyMessage"] = "You Have Subscribed Successfully...";
                return RedirectToAction("Index", "Home");
            }

            // You might want to return something in case 'email' is null
            return RedirectToAction("Index","Home");
        }

        public IActionResult ContactUs()
        {
            return View();
        }




    }


        
    
}
