using Aircon.Data;
using Aircon.Data.Security;
using Microsoft.AspNetCore.Mvc;
using Aircon.Framework.Security;

namespace Aircon.Areas.Admin.Controllers
{

    [PermissionAuthorize(PermissionSystemName.CompanySetting)]
    public class HomeController : BaseAdminController
    {
        private readonly AirconDbContext _airconDbContext;
        public HomeController(AirconDbContext airconDbContext)
        {
            _airconDbContext =airconDbContext;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "User", new { Area = "Admin" });
        }
    }
}
