using Aircon.Controllers.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Aircon.Areas.Customer.Controllers
{
    //[ValidateIpAddress]
    //[AuthorizeAdmin] // Validate if the logged in user in an Internal user
    //[ValidateVendor]
    [AutoValidateAntiforgeryToken]
    [Area("Customer")]
    public abstract partial class BaseCustomerController : AppBaseController
    {

    }
}
