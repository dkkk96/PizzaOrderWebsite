using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaBuy.Controllers
{
    public class PaymentGateway : Controller
    {
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        public IActionResult OrderPlaced()
        {
            return View();
        }
    }

    
}
