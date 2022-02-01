using Microsoft.AspNetCore.Mvc;
using Aircon.Business.Services.Customer;
using Aircon.Data.Helper;
using Aircon.Extensions;
using Aircon.Areas.Customer.Models.Profile;

namespace Aircon.Areas.Customer.Controllers
{
    public class ProfileController : BaseCustomerController
    {
        private readonly IUserProfileService _userProfileService;
        public ProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        public IActionResult Index()
        {
            var userId = HttpContextHelper.UserId;
            if (!userId.HasValue)
                return AccessDeniedView();
            var viewModel = _userProfileService.GetUserProfile(userId.Value).ToViewModel();
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SaveData(UserProfileViewModel profileUserViewModel)
        {
            var result = _userProfileService.SaveUserProfile(profileUserViewModel.ToModel());
            if(result == null)
            {
                ErrorNotification("The save changes failed");
            }
            else
            {
                SuccessNotification("User saved successfully");
            }
            return RedirectToAction("Index");
        }
    }
}
