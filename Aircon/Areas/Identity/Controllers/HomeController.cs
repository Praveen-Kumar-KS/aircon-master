using Microsoft.AspNetCore.Mvc;

namespace Aircon.Areas.Identity.Controllers
{

    public class HomeController : BaseIdentityController
    {

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { Area = "Customer" });
        }
    }
}
