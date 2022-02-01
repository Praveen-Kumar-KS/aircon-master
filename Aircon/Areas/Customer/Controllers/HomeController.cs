using Microsoft.AspNetCore.Mvc;

namespace Aircon.Areas.Customer.Controllers
{
    public class HomeController : BaseCustomerController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
