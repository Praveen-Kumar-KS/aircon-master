using Aircon.Controllers.Shared;
using Aircon.Framework.Security;
using Microsoft.AspNetCore.Mvc;

namespace Aircon.Areas.Admin.Controllers
{
    //[ValidateIpAddress]
    [AuthorizeAdmin] // Validate if the logged in user in an Internal user
    //[ValidateVendor]
    [AutoValidateAntiforgeryToken]
    [Area("Admin")]
    public abstract partial class BaseAdminController : AppBaseController
    {

    }
}
