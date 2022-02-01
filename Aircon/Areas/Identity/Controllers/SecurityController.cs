using Aircon.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aircon.Areas.Identity.Controllers
{
    public class SecurityController : BaseIdentityController
    {
        private readonly IWorkContext _workContext;
        private readonly ILogger<SecurityController> _logger;

        public SecurityController(
            ILogger<SecurityController> logger,
            IWorkContext workContext
        )
        {
            _logger = logger;
            _workContext = workContext;
        }
        public IActionResult AccessDenied(string pageUrl)
        {
            var currentUser = _workContext.CurrentUser;
            if (currentUser == null)
            {
                _logger.LogInformation(string.Format("Access denied to anonymous request on {0}", pageUrl));
                return View();
            }
            _logger.LogInformation(string.Format("Access denied to user #{0} '{1}' on {2}", currentUser.Email, currentUser.Email, pageUrl));
            return View();
        }

    }
}
