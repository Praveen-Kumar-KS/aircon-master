using Aircon.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Aircon.Business.Avatar;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Aircon.Data.Entities;
using Aircon.Data.Helper;

namespace Aircon.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> IndexAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user.IsEmployee)
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            else
                return RedirectToAction("Index", "Home", new { Area = "Customer" });
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContextHelper.Current.User);
    }
}
