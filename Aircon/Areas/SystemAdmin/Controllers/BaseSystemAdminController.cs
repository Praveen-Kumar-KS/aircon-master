using Aircon.Controllers.Shared;
using Aircon.Data.Security;
using Aircon.Framework.Security;
using Microsoft.AspNetCore.Mvc;

namespace Aircon.Areas.SystemAdmin.Controllers
{
    //[ValidateIpAddress]
    //[AuthorizeAdmin] // Validate if the logged in user in an Internal user
    //[ValidateVendor]
    [AutoValidateAntiforgeryToken]
    [Area("SystemAdmin")]
    [PermissionAuthorize(PermissionSystemName.SystemAdmin)]
    public abstract partial class BaseSystemAdminController : AppBaseController
    {

    }
}
