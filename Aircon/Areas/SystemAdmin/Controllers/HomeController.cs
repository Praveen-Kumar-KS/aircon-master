using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.SystemAdmin.Controllers
{
    public class HomeController : BaseSystemAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
