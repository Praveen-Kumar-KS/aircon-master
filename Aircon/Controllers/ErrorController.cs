using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aircon.Controllers
{
    public class ErrorCheckController : Controller
    {
        [AllowAnonymous]
        [HttpGet, HttpPost, Route("~/error")]
        public IActionResult Error(int id)
        {
            return View(id);
        }
    }
}